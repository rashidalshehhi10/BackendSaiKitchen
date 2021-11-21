using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaiKitchenBackend.Controllers;
using Sentry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Constants = BackendSaiKitchen.Helper.Constants;
using Log = Serilog.Log;

namespace BackendSaiKitchen.Controllers
{
    public class PaymentController : BaseController
    {
        public PaymentController(IBlobManager blobManager)
        {
            Helper.Helper.blobManager = blobManager;
        }

        [HttpPost]
        [Route("[action]")]
        public void InvoiceGenerator()
        {
            Helper.Helper.GenerateInvoice();
        }

        [HttpPost]
        [Route("[action]")]
        public object GetQuotationDetailsFromCode(string code)
        {
            int? inquiryIdq = quotationRepository.FindByCondition(x =>
                    (x.QuotationCode == code || x.Inquiry.InquiryCode == code) && x.IsActive == true &&
                    x.IsDeleted == false /*&& x.QuotationStatusId == (int)inquiryStatus.contractApproved*/)
                ?.FirstOrDefault()
                ?.InquiryId;

            int? inquiryId = inquiryRepository.FindByCondition(x =>
                    (x.InquiryCode == code) && x.IsActive == true &&
                    x.IsDeleted == false /*&& x.QuotationStatusId == (int)inquiryStatus.contractApproved*/)
                ?.FirstOrDefault()
                ?.InquiryId;
            if (inquiryIdq != null || inquiryId != null)
            {
                if (inquiryId != null)
                {
                    inquiryIdq = inquiryId;
                }
                List<int> q = inquiryWorkscopeRepository
                    .FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.InquiryId == inquiryIdq)
                    .OrderBy(x => x.WorkscopeId).GroupBy(x => x.WorkscopeId).Select(x => x.Count()).ToList();
                List<TermsAndCondition> terms = termsAndConditionsRepository
                    .FindByCondition(x => x.IsActive == true && x.IsDeleted == false).ToList();
                ViewQuotation viewQuotation = inquiryRepository.FindByCondition(x =>
                        x.InquiryId == inquiryIdq && x.IsActive == true && x.IsDeleted == false)
                    .Select(x => new ViewQuotation
                    {
                        InvoiceNo = "QTN" + x.BranchId + "" + x.CustomerId + "" + x.InquiryId + "" + x.Quotations
                            .OrderBy(y => y.QuotationId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false)
                            .QuotationId,
                        CreatedDate = x.Quotations.OrderBy(y => y.QuotationId)
                            .LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).CreatedDate,
                        ValidDate = x.Quotations.OrderBy(y => y.QuotationId)
                            .LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).QuotationValidityDate,
                        //SerialNo =
                        Description = x.Quotations.OrderBy(y => y.QuotationId)
                            .LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).Description,
                        Discount = x.IsMeasurementPromo == false ? x.PromoDiscount : "0",
                        MeasurementFee = x.Payments.FirstOrDefault(y =>
                            y.PaymentTypeId == (int)paymenttype.Measurement &&
                            y.PaymentStatusId == (int)paymentstatus.PaymentApproved && y.IsActive == true &&
                            y.IsDeleted == false).PaymentAmount.ToString(),
                        Amount = x.Quotations.OrderBy(y => y.QuotationId)
                            .LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).Amount,
                        Vat = x.Quotations.OrderBy(y => y.QuotationId)
                            .LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).Vat,
                        IsInstallment = x.Quotations.OrderBy(y => y.QuotationId)
                            .LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).IsInstallment,
                        //MeasurementFees = x.Payments.OrderBy(y => y.PaymentId).LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).Fees.FeesAmount,
                        AdvancePayment = x.Payments
                            .FirstOrDefault(y =>
                                y.PaymentTypeId == (int)paymenttype.AdvancePayment && y.IsActive == true &&
                                y.IsDeleted == false).PaymentAmountinPercentage.ToString(),
                        BeforeInstallation = x.Payments
                            .FirstOrDefault(y =>
                                y.PaymentTypeId == (int)paymenttype.BeforeInstallation && y.IsActive == true &&
                                y.IsDeleted == false).PaymentAmountinPercentage.ToString(),
                        AfterDelivery = x.Payments
                            .FirstOrDefault(y =>
                                y.PaymentTypeId == (int)paymenttype.AfterDelivery && y.IsActive == true &&
                                y.IsDeleted == false).PaymentAmountinPercentage.ToString(),
                        installments = x.Payments.Where(y =>
                            y.PaymentTypeId == (int)paymenttype.Installment && y.IsActive == true &&
                            y.IsDeleted == false).ToList(),
                        CustomerName = x.Customer.CustomerName,
                        CustomerEmail = x.Customer.CustomerEmail,
                        CustomerContact = x.Customer.CustomerContact,
                        BuildingAddress = x.Building.BuildingAddress,
                        BranchAddress = x.Branch.BranchAddress,
                        BranchContact = x.Branch.BranchContact,
                        ProposalReferenceNumber = x.Quotations.OrderBy(y => y.QuotationId)
                            .LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).ProposalReferenceNumber,
                        TermsAndConditionsDetail = terms,
                        Files = x.Quotations.OrderBy(y => y.QuotationId)
                            .LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).Files.Select(x => x.FileUrl)
                            .ToList(),
                        Quantity =
                            q, //x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false).OrderBy(x => x.WorkscopeId).GroupBy(g => g.WorkscopeId).Select(g => g.Count()).ToList(),
                        inquiryWorkScopeNames = x.InquiryWorkscopes
                            .Where(x => x.IsActive == true && x.IsDeleted == false).OrderBy(x => x.WorkscopeId)
                            .Select(x => x.Workscope.WorkScopeName).ToList(),
                        TotalAmount = x.Quotations.OrderBy(y => y.QuotationId)
                            .LastOrDefault(y => y.IsActive == true && y.IsDeleted == false).TotalAmount,
                        IsQuotation = x.Quotations.Any() ? true : false
                    }).FirstOrDefault();
                if (viewQuotation != null)
                {
                    Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                            x.InquiryId == inquiryId && x.IsActive == true && x.IsDeleted == false)
                        .Include(x => x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                        .ThenInclude(x => x.Designs.Where(y => y.IsActive == true && y.IsDeleted == false))
                        .ThenInclude(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                        .Include(x => x.JobOrders.Where(y => y.IsActive == true && y.IsDeleted == false))
                        .FirstOrDefault();
                    foreach (InquiryWorkscope inws in inquiry.InquiryWorkscopes)
                    {
                        foreach (Design design in inws.Designs)
                        {
                            foreach (File file in design.Files)
                            {
                                viewQuotation.Files.Add(file.FileUrl);
                            }
                        }
                    }

                    foreach (JobOrder job in inquiry.JobOrders)
                    {
                        if (job.MepdrawingFileUrl != string.Empty || job.MepdrawingFileUrl != null)
                        {
                            viewQuotation.Files.Add(job.MepdrawingFileUrl);
                        }
                    }

                    try
                    {
                        viewQuotation.invoiceDetails = new List<InvoiceDetail>();
                        for (int i = 0; i < viewQuotation.inquiryWorkScopeNames.Count; i++)
                        {
                            viewQuotation.invoiceDetails.Add(new InvoiceDetail
                            {
                                inquiryWorkScopeNames = viewQuotation.inquiryWorkScopeNames[i],
                                Quantity = viewQuotation.Quantity[i]
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message);
                    }

                    viewQuotation.TermsAndConditionsDetail.RemoveAll(x =>
                        x.IsInstallmentTerms != viewQuotation.IsInstallment);

                    if (viewQuotation.IsInstallment == true)
                    {
                        for (int i = 0; i < viewQuotation.installments.Count - 1; i++)
                        {
                            viewQuotation.TermsAndConditionsDetail.Add(new TermsAndCondition
                            {
                                TermsAndConditionsDetail =
                                    viewQuotation.TermsAndConditionsDetail[1].TermsAndConditionsDetail,
                                IsInstallmentTerms = true
                            });
                        }
                    }

                    int j = -1;
                    viewQuotation.TermsAndConditionsDetail.ForEach(x =>
                    {
                        if (x.IsInstallmentTerms == viewQuotation.IsInstallment)
                        {
                            if (viewQuotation.IsInstallment == false)
                            {
                                x.TermsAndConditionsDetail = x.TermsAndConditionsDetail
                                    .Replace("[AdvancePayment]", viewQuotation.AdvancePayment + "%")
                                    .Replace("[BeforeInstallation]", viewQuotation.BeforeInstallation + "%")
                                    .Replace("[AfterDelivery]", viewQuotation.AfterDelivery + "%");
                            }
                            else
                            {
                                if (j == -1)
                                {
                                    x.TermsAndConditionsDetail = x.TermsAndConditionsDetail.Replace("[AdvancePayment]",
                                        viewQuotation.AdvancePayment + "%");
                                }
                                else
                                {
                                    x.TermsAndConditionsDetail = x.TermsAndConditionsDetail
                                        .Replace("[noofInstallment]", j + 1 + "")
                                        .Replace("[installmentPercentage]",
                                            viewQuotation.installments[j].PaymentAmountinPercentage + "%")
                                        .Replace("[installmentDate]",
                                            viewQuotation.installments[j].PaymentExpectedDate + "");
                                }

                                j++;
                            }
                        }
                    });
                    response.data = viewQuotation;
                }
                else
                {
                    response.errorMessage = "Inquiry doesnt exist";
                    response.isError = true;
                }
            }
            else
            {
                response.errorMessage = "Inquiry doesnt exist";
                response.isError = true;
            }

            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetUnPaidPaymentByCode(string code)
        {
            response.data = quotationRepository
                .FindByCondition(x =>
                    (x.QuotationCode == code || x.Inquiry.InquiryCode == code) &&
                    x.QuotationStatusId == (int)inquiryStatus.contractApproved && x.IsActive == true &&
                    x.IsDeleted == false && x.Payments.Any(y =>
                        y.IsActive == true && y.IsDeleted == false &&
                        y.PaymentStatusId != (int)paymentstatus.PaymentApproved &&
                        y.PaymentStatusId != (int)paymentstatus.InstallmentApproved))
                .Include(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false)).Include(x =>
                    x.Payments.Where(y =>
                        y.PaymentStatusId != (int)paymentstatus.PaymentApproved &&
                        y.PaymentStatusId != (int)paymentstatus.InstallmentApproved && y.IsActive == true &&
                        y.IsDeleted == false)).FirstOrDefault();
            if (response.data == null)
            {
                response.isError = true;
                response.errorMessage = "No Pending Payments";
            }

            return response;
        }
        //[HttpPost]
        //[Route("[action]")]
        //public void TestPayment(decimal? amount)
        //{
        //    Helper.Helper.AddPayment(amount);
        //}

        //[AuthFilter((int)permission.ManagePayment, (int)permissionLevel.Read)]
        [HttpPost]
        [Route("[action]")]
        public object GetAllInquiryForPayment()
        {
            List<IGrouping<int?, Payment>> payments = paymentRepository.FindByCondition(x =>
                x.PaymentStatusId != (int)paymentstatus.PaymentApproved &&
                x.PaymentStatusId != (int)paymentstatus.InstallmentApproved && x.IsActive == true &&
                x.IsDeleted == false).GroupBy(x => x.InquiryId).ToList();

            if (payments != null)
            {
                response.data = payments;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "payments does not Exist";
            }

            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> AddPayment(Payment payment)
        {
            Inquiry inquiry = inquiryRepository
                .FindByCondition(x => x.InquiryId == payment.InquiryId && x.IsActive == true && x.IsDeleted == false)
                .FirstOrDefault();
            Payment _payment = new Payment();

            foreach (File file in payment.Files)
            {
                Tuple<string, string> fileUrl = await Helper.Helper.UploadFile(file.FileImage);

                if (fileUrl != null)
                {
                    payment.Files.Add(new File
                    {
                        FileUrl = fileUrl.Item1,
                        FileName = fileUrl.Item1.Split('.')[0],
                        FileContentType = fileUrl.Item2,
                        IsImage = true,
                        IsActive = true,
                        IsDeleted = false,
                        UpdatedBy = Constants.userId,
                        UpdatedDate = Helper.Helper.GetDateTime(),
                        CreatedBy = Constants.userId,
                        CreatedDate = Helper.Helper.GetDateTime()
                    });
                }
                else
                {
                    response.isError = true;
                    response.errorMessage = Constants.wrongFileUpload;
                }
            }

            List<int?> roletypeId = new List<int?>
            {
                (int)roleType.Manager
            };

            try
            {
                sendNotificationToHead(content: Constants.PaymentAdded, false,
                    null,
                    null,
                    roletypeId, Constants.branchId, (int)notificationCategory.Other);
            }
            catch (Exception e)
            {
                SentrySdk.CaptureMessage(e.Message);
            }

            _payment.CreatedBy = payment.CreatedBy;
            _payment.UpdatedBy = payment.UpdatedBy;
            _payment.PaymentName = payment.PaymentName;
            _payment.PaymentStatus = payment.PaymentStatus;
            _payment.PaymentStatusId = (int)paymentstatus.PaymentCreated;
            _payment.PaymentTypeId = payment.PaymentTypeId;
            _payment.InquiryId = payment.InquiryId;
            _payment.PaymentModeId = payment.PaymentModeId;
            _payment.IsActive = true;
            _payment.IsDeleted = false;
            inquiry.Payments.Add(_payment);
            inquiryRepository.Update(inquiry);
            context.SaveChanges();
            response.data = inquiry;
            return response;
        }


        [HttpPost]
        [Route("[action]")]
        public object AcceptPayment(UpdatePaymentStatus updatePayment)
        {
            Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                x.InquiryId == updatePayment.InquiryId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (inquiry != null)
            {
                try
                {
                    Payment payment = inquiry.Payments?.Where(x =>
                        x.PaymentId == updatePayment.PaymentId && x.IsActive == true && x.IsDeleted == false &&
                        x.PaymentStatusId == (int)paymentstatus.PaymentWaitingofApproval).FirstOrDefault();
                    payment.PaymentStatusId = (int)paymentstatus.PaymentApproved;
                    inquiryRepository.Update(inquiry);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    SentrySdk.CaptureMessage(e.Message);
                    response.errorMessage = "Payment does not exist";
                    response.isError = true;
                }
            }
            else
            {
                response.errorMessage = "Inquiry does not exsit";
                response.isError = true;
            }

            return response;
        }


        [HttpPost]
        [Route("[action]")]
        public object DeclinePayment(UpdatePaymentStatus updatePayment)
        {
            Inquiry inquiry = inquiryRepository.FindByCondition(x =>
                x.InquiryId == updatePayment.InquiryId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (inquiry != null)
            {
                try
                {
                    Payment payment = inquiry.Payments?.Where(x =>
                        x.PaymentId == updatePayment.PaymentId && x.IsActive == true && x.IsDeleted == false &&
                        x.PaymentStatusId == (int)paymentstatus.PaymentWaitingofApproval).FirstOrDefault();
                    payment.PaymentStatusId = (int)paymentstatus.PaymentRejected;
                    inquiryRepository.Update(inquiry);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    SentrySdk.CaptureMessage(e.Message);
                    response.errorMessage = "Payment does not exist";
                    response.isError = true;
                }
            }
            else
            {
                response.errorMessage = "Inquiry does not exsit";
                response.isError = true;
            }

            return response;
        }


        [HttpPost]
        [Route("[action]")]
        public object GetInquiryForPaymentById(int inquiryId)
        {
            Inquiry inquiry = inquiryRepository
                .FindByCondition(x => x.InquiryId == inquiryId && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.Payments.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            if (inquiry != null)
            {
                response.data = inquiry;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "inquiry Does not Exist";
            }

            return response;
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<object> GenerateSalesInvoicePaymentByIdAsync(SalesInvoiceRequest salesInvoiceRequest)
        {
            var payment = paymentRepository
                .FindByCondition(x =>
                    x.PaymentId == salesInvoiceRequest.PaymentId && x.IsActive == true && x.IsDeleted == false).Select(
                    x => new SalesInvoiceReciept
                    {
                        InvoiceCode = ("REF" + x.Quotation.QuotationCode + x.PaymentId).ToString().Replace("QTN", ""),
                        InquiryCode = x.Quotation.Inquiry.InquiryCode,
                        CreatedDate = Helper.Helper.GetDateTime(),
                        CustomerName = x.Quotation.Inquiry.Customer.CustomerName,
                        CustomerContact = x.Quotation.Inquiry.Customer.CustomerContact,
                        CustomerEmail = x.Quotation.Inquiry.Customer.CustomerEmail,
                        BuildiingAddress = x.Quotation.Inquiry.Building.BuildingAddress,
                        WorkscopeName = x.Quotation.Inquiry.InquiryWorkscopes
                            .Where(y => y.IsActive == true && y.IsDeleted == false)
                            .Select(y => y.Workscope.WorkScopeName).ToList(),
                        Amount = x.Quotation.Amount,
                        Discount = x.Quotation.Discount,
                        VAT = x.Quotation.Vat,
                        Deduction = x.Quotation.Inquiry.MeasurementFees,
                        TotalAmount = x.Quotation.TotalAmount,
                        AmounttoBePaid = x.PaymentAmount
                    });
             if (payment.Any())
            {
                try
                {
                    if (salesInvoiceRequest.PaymentModeId == (int)paymentMode.OnlinePayment)
                    {

                        string customerEmail = payment.FirstOrDefault().CustomerEmail;
                        await mailService.SendEmailAsync(new MailRequest
                        {
                            ToEmail = payment.FirstOrDefault().CustomerEmail,
                            Subject = "Pay Online using Link",
                            Body = "https://saikitchen.azurewebsites.net/onlinepay.html?paymentId="+ salesInvoiceRequest.PaymentId
                        });
                    }
                }
                catch (Exception)
                {

                }

                response.data = payment;
            }
            else
            {
                response.errorMessage = "Payment Doesnt exist";
                response.isError = true;
            }

            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> UploadInvoice(Invoice invoice)
        {
            List<IFormFile> files = new List<IFormFile>();
            Payment payment = paymentRepository
                .FindByCondition(x => x.PaymentId == invoice.PaymentId && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.Inquiry).ThenInclude(x =>
                    x.InquiryWorkscopes.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Files.Where(y => y.IsActive == true && y.IsDeleted == false))
                .Include(x => x.Inquiry).ThenInclude(x => x.Customer).FirstOrDefault();

            if (payment != null)
            {
                if (invoice.Files != null)
                {
                    foreach (string fileUrl in invoice.Files)
                    {
                        //var fileUrl = await Helper.Helper.UploadFile(file);
                        if (fileUrl != null)
                        {
                            payment.Files.Add(new File
                            {
                                FileUrl = fileUrl,
                                FileName = fileUrl.Split('.')[0],
                                FileContentType = fileUrl.Split('.').Length > 1 ? fileUrl.Split('.')[1] : "mp4",
                                IsImage = fileUrl.Split('.').Length > 1,
                                IsActive = true,
                                IsDeleted = false,
                                UpdatedBy = Constants.userId,
                                UpdatedDate = Helper.Helper.GetDateTime(),
                                CreatedBy = Constants.userId,
                                CreatedDate = Helper.Helper.GetDateTime()
                            });

                            files.Add(Helper.Helper.ConvertBytestoIFormFile(await Helper.Helper.GetFile(fileUrl)));
                        }
                    }

                    payment.PaymentModeId = invoice.PaymentModeId;
                    payment.IsAmountRecieved = invoice.PaymentModeId == (int)paymentMode.Cheque ? false : true;
                    payment.AmountRecievedBy = Constants.userId;
                    payment.AmountRecievedDate = Helper.Helper.GetDateTime();
                    payment.PaymentStatusId = (int)paymentstatus.PaymentApproved;
                    payment.PaymentDoneBy = invoice.userId;
                    payment.PaymentCompletionDate = Helper.Helper.GetDateTime();
                    if (payment.PaymentTypeId == (int)paymenttype.AdvancePayment)
                    {
                        payment.Inquiry.InquiryStatusId = (int)inquiryStatus.checklistPending;
                        foreach (InquiryWorkscope inquiryWorkscope in payment.Inquiry.InquiryWorkscopes)
                        {
                            inquiryWorkscope.InquiryStatusId = (int)inquiryStatus.checklistPending;
                        }
                    }

                    if ((bool)payment.IsAmountRecieved)
                    {
                        try
                        {
                            await mailService.SendEmailAsync(new MailRequest
                            {
                                Subject = "Payment Files",
                                ToEmail = payment.Inquiry.Customer.CustomerEmail,
                                Body = "Payment Files",
                                Attachments = files
                            });
                        }
                        catch (Exception ex)
                        {
                            SentrySdk.CaptureMessage(ex.Message);
                        }
                    }
                    

                    paymentRepository.Update(payment);
                    context.SaveChanges();
                }
                else
                {
                    response.isError = true;
                    response.errorMessage = "Please Add Files";
                }
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Payment Not Found";
            }

            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetPaymentDetailsByPaymentId(int PaymentId)
        {
            var payment = paymentRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.PaymentId == PaymentId).Select(x => new
            {
                PaymentId = PaymentId,
                InquiryId = x.InquiryId,
                InquiryCode = x.Inquiry.InquiryCode,
                InvoiceNo = "QTN" + x.Inquiry.BranchId + "" + x.Inquiry.CustomerId + "" + x.InquiryId + "" + x.Inquiry.Quotations.
                        FirstOrDefault(y => y.IsActive == true && y.IsDeleted == false)
                        .QuotationId,
                CreatedDate = x.Inquiry.Quotations
                        .FirstOrDefault(y => y.IsActive == true && y.IsDeleted == false).CreatedDate,
                ValidDate = x.Inquiry.Quotations
                        .FirstOrDefault(y => y.IsActive == true && y.IsDeleted == false).QuotationValidityDate,
                CustomerName = x.Inquiry.Customer.CustomerName,
                CustomerEmail = x.Inquiry.Customer.CustomerEmail,
                CustomerContact = x.Inquiry.Customer.CustomerContact,
                CustomerWhatsapp = x.Inquiry.Customer.CustomerWhatsapp,
                ProposalReferenceNumber = x.Inquiry.Quotations
                        .FirstOrDefault(y => y.IsActive == true && y.IsDeleted == false).ProposalReferenceNumber,
                PaidAmount = x.Inquiry.Payments.Where(y => y.IsActive == true && y.IsDeleted == false && (y.PaymentStatusId == (int)paymentstatus.InstallmentApproved || y.PaymentStatusId == (int)paymentstatus.PaymentApproved)).Sum(x => x.PaymentAmount) / 100,
                PaymentAmount = x.PaymentAmount / 100,
                PaymnetMode = x.PaymentModeId,
                Paymentstatus = x.PaymentStatus.PaymentStatusName,
                Paymenttype = x.PaymentType.PaymentTypeName,
                TotalAmount = x.Inquiry.Quotations.FirstOrDefault(y => y.IsActive == true && y.IsDeleted == false).TotalAmount,
                Amount = x.Inquiry.Quotations.FirstOrDefault(y => y.IsActive == true && y.IsDeleted == false).Amount,
                inquiryWorkScopeNames = x.Inquiry.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)
                        .OrderBy(x => x.WorkscopeId).Select(x => x.Workscope.WorkScopeName).ToList(),

            }).FirstOrDefault();
            if (payment != null)
            {
                response.data = payment;
            }
            else
            {
                response.isError = true;
                response.errorMessage = " Payment Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object AddPaymentByPaymentId(addPayment add)
        {
            var payment = paymentRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.PaymentId == add.paymentId)
                .Include(x => x.Inquiry).ThenInclude(x => x.InquiryWorkscopes.Where(x => x.IsActive == true && x.IsDeleted == false)).FirstOrDefault();
            if (payment != null)
            {
                if (payment.PaymentTypeId == (int)paymenttype.Installment)
                {
                    payment.PaymentStatusId = (int)paymentstatus.InstallmentApproved;
                    payment.ClientSecret = add.ClientSecret;
                    payment.PaymentIntentToken = add.PaymentIntentToken;
                    payment.PaymentMethod = add.PaymentMethod;
                    payment.PaymentModeId = add.SelectedPaymentMode;
                }
                else if (payment.PaymentTypeId == (int)paymenttype.AdvancePayment)
                {
                    payment.PaymentStatusId = (int)paymentstatus.PaymentApproved;
                    payment.ClientSecret = add.ClientSecret;
                    payment.PaymentIntentToken = add.PaymentIntentToken;
                    payment.PaymentMethod = add.PaymentMethod;
                    payment.PaymentModeId = add.SelectedPaymentMode;
                    payment.Inquiry.InquiryStatusId = (int)inquiryStatus.checklistPending;
                    Helper.Helper.Each(payment.Inquiry.InquiryWorkscopes, x => x.InquiryStatusId = (int)inquiryStatus.checklistPending);
                }
                else
                {
                    payment.PaymentStatusId = (int)paymentstatus.PaymentApproved;
                    payment.ClientSecret = add.ClientSecret;
                    payment.PaymentIntentToken = add.PaymentIntentToken;
                    payment.PaymentMethod = add.PaymentMethod;
                    payment.PaymentModeId = add.SelectedPaymentMode;
                }
                response.data = "Payment Approved";
                paymentRepository.Update(payment);
                context.SaveChanges();
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Payment Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetPaymentUnRecieved(int inquiryId)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == inquiryId && x.IsActive == true && x.IsActive == true && x.Payments.Any(y => y.IsActive == true && y.IsDeleted == false && y.IsAmountRecieved == false && (y.PaymentStatusId != (int)paymentstatus.InstallmentApproved || y.PaymentStatusId != (int)paymentstatus.PaymentApproved) && y.PaymentModeId == (int)paymentMode.Cheque))
                .Include(x => x.Payments.Where(y => y.IsActive == true && y.IsDeleted == false && y.IsAmountRecieved == false && (y.PaymentStatusId != (int)paymentstatus.InstallmentApproved || y.PaymentStatusId != (int)paymentstatus.PaymentApproved) && y.PaymentModeId == (int)paymentMode.Cheque))
                .FirstOrDefault();
            if (inquiry != null)
            {
                response.data = inquiry;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "No Payments For This inquiry";
            }
            return response;
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<object> PaymentRecieveAmount(int paymentId)
        {
            var payment = paymentRepository.FindByCondition(x => x.PaymentId == paymentId && x.IsActive == true && x.IsDeleted == false && x.IsAmountRecieved == false && (x.PaymentStatusId != (int)paymentstatus.InstallmentApproved || x.PaymentStatusId != (int)paymentstatus.PaymentApproved) && x.PaymentModeId == (int)paymentMode.Cheque).FirstOrDefault();
            if (payment != null)
            {
                payment.IsAmountRecieved = true;
                payment.AmountRecievedBy = Constants.userId;
                payment.AmountRecievedDate = Helper.Helper.GetDateTime();

                try
                {
                    await mailService.SendEmailAsync(new MailRequest
                    {
                        Subject = "Payment Files",
                        ToEmail = payment.Inquiry.Customer.CustomerEmail,
                        Body = "Payment Files",
                        //Attachments = files
                    });
                }
                catch (Exception ex)
                {
                    SentrySdk.CaptureMessage(ex.Message);
                }
                paymentRepository.Update(payment);
                context.SaveChanges();
                response.data = "Amount Recieved";
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Payment Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetPaymentDetails(int paymentId)
        {
            List<TermsAndCondition> terms = termsAndConditionsRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false)
                .ToList();
            var _quotation = paymentRepository.FindByCondition(x => x.PaymentId == paymentId && x.IsDeleted == false && x.IsActive == true).Include(x => x.Quotation).ThenInclude(x => x.Payments.Where(y => y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
            var payment = paymentRepository.FindByCondition(x => x.PaymentId == paymentId && x.IsActive == true && x.IsDeleted == false).Select(x => new
            {
                CustomerName = x.Inquiry.Customer.CustomerName,
                TransactionNumber = x.Quotation.QuotationCode,
                ProposalReferenceNumber = x.Quotation.ProposalReferenceNumber,
                Amount = x.PaymentAmount / 100,
                PaymentMethod = x.PaymentModeId,
                PaymentDescreption = x.PaymentDetail,
                InquiryCode = x.Inquiry.InquiryCode,
                CouponCode = x.Inquiry.Promo.PromoCode == null ? string.Empty : x.Inquiry.Promo.PromoCode,
                PaymentDate = x.AmountRecievedDate == null ? string.Empty : x.AmountRecievedDate,
                TermsAndConditionsDetail = terms,
            }).FirstOrDefault();

            payment.TermsAndConditionsDetail.RemoveAll(x =>
                    x.IsInstallmentTerms != _quotation.Quotation.IsInstallment);
            

            if (_quotation.Quotation.IsInstallment == true)
            {
                for (int i = 0; i < _quotation.Quotation.NoOfInstallment - 1; i++)
                {
                    payment.TermsAndConditionsDetail.Add(new TermsAndCondition
                    {
                        TermsAndConditionsDetail =
                            payment.TermsAndConditionsDetail[1].TermsAndConditionsDetail,
                        IsInstallmentTerms = true
                    });
                }
            }

            int j = -1;
            List<TermsAndCondition> deletedTermsAndConditions = new List<TermsAndCondition>();
            payment.TermsAndConditionsDetail.ForEach(x =>
            {
                if (x.IsInstallmentTerms == _quotation.Quotation.IsInstallment)
                {
                    if (_quotation.Quotation.IsInstallment == false)
                    {
                        if (x.TermsAndConditionsDetail.Contains("[AdvancePayment]") &&
                            (_quotation.Quotation.AdvancePayment == null || _quotation.Quotation.AdvancePayment == "0"))
                        {
                            //viewQuotation.TermsAndConditionsDetail.Remove(x);
                            deletedTermsAndConditions.Add(x);
                        }
                        else if (x.TermsAndConditionsDetail.Contains("[BeforeInstallation]") &&
                                 (_quotation.Quotation.BeforeInstallation == null ||
                                  _quotation.Quotation.BeforeInstallation == "0"))
                        {
                            //viewQuotation.TermsAndConditionsDetail.Remove(x);
                            deletedTermsAndConditions.Add(x);
                        }
                        else if (x.TermsAndConditionsDetail.Contains("[AfterDelivery]") &&
                                 (_quotation.Quotation.AfterDelivery == null || _quotation.Quotation.AfterDelivery == "0"))
                        {
                            //viewQuotation.TermsAndConditionsDetail.Remove(x);
                            deletedTermsAndConditions.Add(x);
                        }

                        x.TermsAndConditionsDetail = x.TermsAndConditionsDetail
                            .Replace("[AdvancePayment]", _quotation.Quotation.AdvancePayment + "%")
                            .Replace("[BeforeInstallation]", _quotation.Quotation.BeforeInstallation + "%")
                            .Replace("[AfterDelivery]", _quotation.Quotation.AfterDelivery + "%");
                    }
                    else
                    {
                        if (j == -1)
                        {
                            if (_quotation.Quotation.NoOfInstallment >= 1)
                            {
                                x.TermsAndConditionsDetail = x.TermsAndConditionsDetail.Replace("[AdvancePayment]",
                                _quotation.Quotation.Payments.FirstOrDefault(y =>
                            y.PaymentTypeId == (int)paymenttype.AdvancePayment && y.IsActive == true &&
                            y.IsDeleted == false)?.PaymentAmountinPercentage?.ToString() + "% ");
                                j++;
                            }

                        }
                        else
                        {
                            var c = _quotation.Quotation.AdvancePayment == "0" ? j + 1 : j;
                            
                            x.TermsAndConditionsDetail = x.TermsAndConditionsDetail
                                .Replace("[noofInstallment]", c  + "")
                                .Replace("[installmentPercentage]",
                                    _quotation.Quotation.Payments.OrderBy(x => x.PaymentId).ToList()[j].PaymentAmountinPercentage + "%")
                                .Replace("[installmentDate]",
                                    _quotation.Quotation.Payments.OrderBy(x => x.PaymentId).ToList()[j].PaymentExpectedDate + "");
                        }
                        j++;


                    }
                }
            });
            deletedTermsAndConditions.ForEach(x => payment.TermsAndConditionsDetail.Remove(x));
            if (_quotation.Quotation.AdvancePayment == "0")
            {
                payment.TermsAndConditionsDetail.RemoveAll(x => x.TermsAndConditionsDetail.Contains("Advance"));
            }

            if (payment != null)
            {
                response.data = payment;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "Payment Not Found";
            }
            return response;
        }

        [HttpPost]
        [Route("[action]")]
        public object AddPaymentBeforeQuotation(beforeQuotation before)
        {
            var inquiry = inquiryRepository.FindByCondition(x => x.IsActive == true && x.IsDeleted == false && x.InquiryId == before.inquiryId)
                .Include(x => x.Payments.Where(x => x.IsActive == true && x.IsDeleted == false)).FirstOrDefault();
            if (inquiry != null)
            {
                Payment payment = new Payment();

                foreach (string fileUrl in before.files)
                {
                    //var fileUrl = await Helper.Helper.UploadFile(file);
                    if (fileUrl != null)
                    {
                        payment.Files.Add(new File
                        {
                            FileUrl = fileUrl,
                            FileName = fileUrl.Split('.')[0],
                            FileContentType = fileUrl.Split('.').Length > 1 ? fileUrl.Split('.')[1] : "mp4",
                            IsImage = fileUrl.Split('.').Length > 1,
                            IsActive = true,
                            IsDeleted = false,
                            UpdatedBy = Constants.userId,
                            UpdatedDate = Helper.Helper.GetDateTime(),
                            CreatedBy = Constants.userId,
                            CreatedDate = Helper.Helper.GetDateTime()
                        });

                    }
                }
                payment.IsActive = true;
                payment.IsDeleted = false;
                payment.CreatedBy = Constants.userId;
                payment.CreatedDate = Helper.Helper.GetDateTime();
                payment.PaymentAmount =(decimal)before.amount;
                payment.PaymentStatusId = (int)paymentstatus.PaymentApproved;
                inquiry.Payments.Add(payment);
                inquiryRepository.Update(inquiry);
                context.SaveChanges();
                response.data = payment;
            }
            else
            {
                response.isError = true;
                response.errorMessage = "inquiry Not Found";
            }
            return response;
        }

    }
}