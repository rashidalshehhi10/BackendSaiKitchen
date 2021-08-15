using BackendSaiKitchen.ActionFilters;
using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaiKitchenBackend.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public object GetPaymentByCode(string code)
        {
            response.data = quotationRepository.FindByCondition(x => (x.QuotationCode == code || x.Inquiry.InquiryCode == code) && x.IsActive == true && x.IsDeleted == false).Include(x => x.Payments.Where(y => (y.PaymentStatusId != (int)paymentstatus.PaymentApproved && y.PaymentStatusId != (int)paymentstatus.InstallmentApproved) && y.IsActive == true && y.IsDeleted == false)).FirstOrDefault();
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
            var payments = paymentRepository.FindByCondition(x => x.PaymentStatusId != (int)paymentstatus.PaymentApproved && x.PaymentStatusId != (int)paymentstatus.InstallmentApproved && x.IsActive == true && x.IsDeleted == false).GroupBy(x => x.InquiryId).ToList();

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
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == payment.InquiryId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            Payment _payment = new Payment();

            foreach (var file in payment.Files)
            {
                var fileUrl = await Helper.Helper.UploadFile(file.FileImage);

                if (fileUrl != null)
                {
                    payment.Files.Add(new File()
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
                        CreatedDate = Helper.Helper.GetDateTime(),
                    });
                }
                else
                {
                    response.isError = true;
                    response.errorMessage = Constants.wrongFileUpload;
                }
            }

            List<int?> roletypeId = new List<int?>();
            roletypeId.Add((int)roleType.Manager);

            try
            {
                sendNotificationToHead(Constants.PaymentAdded, false,
                    null,
                    null,
                   roletypeId, Constants.branchId, (int)notificationCategory.Other);
            }
            catch (Exception e)
            {
                Sentry.SentrySdk.CaptureMessage(e.Message);
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
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == updatePayment.InquiryId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (inquiry != null)
            {

                try
                {
                    var payment = inquiry.Payments?.Where(x => x.PaymentId == updatePayment.PaymentId && x.IsActive == true && x.IsDeleted == false && x.PaymentStatusId == (int)paymentstatus.PaymentWaitingofApproval).FirstOrDefault();
                    payment.PaymentStatusId = (int)paymentstatus.PaymentApproved;
                    inquiryRepository.Update(inquiry);
                    context.SaveChanges();

                }
                catch (Exception e)
                {
                    Sentry.SentrySdk.CaptureMessage(e.Message);
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
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == updatePayment.InquiryId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (inquiry != null)
            {

                try
                {
                    var payment = inquiry.Payments?.Where(x => x.PaymentId == updatePayment.PaymentId && x.IsActive == true && x.IsDeleted == false && x.PaymentStatusId == (int)paymentstatus.PaymentWaitingofApproval).FirstOrDefault();
                    payment.PaymentStatusId = (int)paymentstatus.PaymentRejected;
                    inquiryRepository.Update(inquiry);
                    context.SaveChanges();

                }
                catch (Exception e)
                {
                    Sentry.SentrySdk.CaptureMessage(e.Message);
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
            var inquiry = inquiryRepository.FindByCondition(x => x.InquiryId == inquiryId & x.IsActive == true && x.IsDeleted == false)
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
    }
}
