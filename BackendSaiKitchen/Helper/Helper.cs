﻿using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NPOI.XWPF.UserModel;
using Sentry;
using Stripe;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VimeoDotNet;
using VimeoDotNet.Net;
using VimeoDotNet.Models;
using static BackendSaiKitchen.CustomModel.VimeoCustomModel;
using VimeoDotNet.Authorization;
using System.Net.Http;

namespace BackendSaiKitchen.Helper
{
    public class Helper
    {
        private static readonly CultureInfo provider = CultureInfo.InvariantCulture;
        public static IBlobManager blobManager;


        private static readonly TimeZoneInfo UAETimeZone = TimeZoneInfo.FindSystemTimeZoneById("Arabian Standard Time");
        private readonly List<XWPFParagraph> paragraphs = new();

        public static string GenerateToken(int userId)
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            byte[] id = Encoding.ASCII.GetBytes(userId.ToString());
            return Convert.ToBase64String(id.Concat(time.Concat(key)).ToArray());
        }

        public static string GetDateTime()
        {
            DateTime utc = DateTime.UtcNow;
            DateTime UAE = TimeZoneInfo.ConvertTimeFromUtc(utc, UAETimeZone);
            //04 / 27 / 2021 10:01 AM
            return UAE.ToString("MM/dd/yyyy hh:mm tt");
        }

        public static string GetDate()
        {
            DateTime utc = DateTime.UtcNow;
            DateTime UAE = TimeZoneInfo.ConvertTimeFromUtc(utc, UAETimeZone);
            //04 / 27 / 2021 10:01 AM
            return UAE.ToString("MM/dd/yyyy");
        }

        public static DateTime ConvertToDateTime(string dateTime)
        {
            //04 / 27 / 2021 10:01 AM
            DateTime.TryParseExact(dateTime, new[] { "MM/dd/yyyy hh:mm tt", "MM/dd/yyyy h:mm tt", "MM/dd/yyyy" },
                provider, DateTimeStyles.None, out DateTime dateTimeParsed);

            return dateTimeParsed;
        }

        public static string GetDateFromString(string dateTime)
        {
            //04 / 27 / 2021 10:01 AM
            if (dateTime != null)
            {
                return DateTime.ParseExact(dateTime, "MM/dd/yyyy hh:mm tt", null).ToString("MM/dd/yyyy");
            }

            return "";
        }

        public static object SetResponse(object obj)
        {
            //string json = JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings
            //{
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //});

            //json = json.Replace(@"\", " ");
            return obj;
        }

        public static string DecryptString(string encrString)
        {
            byte[] b;
            string decrypted;
            try
            {
                b = Convert.FromBase64String(encrString);
                decrypted = Encoding.ASCII.GetString(b);
            }
            catch (FormatException)
            {
                decrypted = "";
            }

            return decrypted;
        }

        public static string EnryptString(string strEncrypted)
        {
            byte[] b = Encoding.ASCII.GetBytes(strEncrypted);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }


        public static async Task<object> DeleteFile(string fileUrl)
        {
            string result = "";
            if (fileUrl != null)
            {
                if (fileUrl.Contains('.'))
                {
                    result = "File " + await DeleteFileFromBlob(fileUrl).ConfigureAwait(false) + " Has Been Deleted";
                }
                else if (long.TryParse(fileUrl, out long videoId))
                {
                    result = "Video " + await DeleteVideo(videoId).ConfigureAwait(false) + " Has Been Deleted";
                }
                else
                {
                    throw new FileNotFoundException(Constants.wrongFileUpload);
                }
            }
            else
            {
                throw new FileNotFoundException(Constants.MeasurementFileMissing);
            }

            return result;
        }


        public static async Task<Tuple<string, string>> UploadFile(byte[] fileByte)
        {
            string fileUrl = "";
            string ext = "";
            if (fileByte != null)
            {
                ext = GuessFileType(fileByte);
                if (ext == "png" || ext == "jpg" || ext == "pdf" || ext == "dwg")
                {
                    fileUrl = await PostFile(fileByte, ext).ConfigureAwait(false);
                }
                //fileUrl = await UploadFileToBlob(fileByte, ext);
                else if (ext == "mp4")
                {
                    fileUrl = await UploadUpdateVideo(fileByte).ConfigureAwait(false);
                }
                else
                {
                    throw new FileNotFoundException(Constants.wrongFileUpload);
                }
            }
            else
            {
                throw new FileNotFoundException(Constants.MeasurementFileMissing);
            }

            return new Tuple<string, string>(fileUrl, ext);
        }

        public static IFormFile ConvertBytestoIFormFile(byte[] fileByte)
        {
            string fileUrl = "";
            string ext = GuessFileType(fileByte);
            MemoryStream stream = new MemoryStream(fileByte);
            fileUrl = Guid.NewGuid() + "." + ext;
            IFormFile blob = new FormFile(stream, 0, fileByte.Length, "azure", fileUrl)
            {
                Headers = new HeaderDictionary(),
                ContentType = ext == "pdf" ? "application/" + ext :
                    ext == "dwg" ? "application/octet-stream" : "image/" + ext
            };

            return blob;
        }

        public static async Task<string> UploadFileToBlob(byte[] fileByte, string ext)
        {
            string fileUrl = "";
            try
            {
                MemoryStream stream = new MemoryStream(fileByte);
                fileUrl = Guid.NewGuid() + "." + ext;
                IFormFile blob = new FormFile(stream, 0, fileByte.Length, "azure", fileUrl);
                await blobManager.Upload(new Blob { File = blob });
            }
            catch (Exception e)
            {
                SentrySdk.CaptureMessage(e.Message);
            }

            return fileUrl;
        }


        public static async Task<Tuple<string, string>> UploadFormDataFile(byte[] fileByte, string ext)
        {
            string fileUrl = "";
            //string ext = "";
            if (fileByte != null)
            {
                if (ext.ToLower().Contains("png") || ext.ToLower().Contains("jpg") || ext.ToLower().Contains("jpeg") ||
                    ext.ToLower().Contains("pdf") || ext.ToLower().Contains("dwg"))
                {
                    fileUrl = await PostFile(fileByte, ext).ConfigureAwait(false);
                }
                //fileUrl = await UploadFileToBlob(fileByte, ext);
                else if (ext.ToLower().Contains("mp4"))
                {
                    fileUrl = await UploadUpdateVideo(fileByte).ConfigureAwait(false);
                }
                else
                {
                    throw new FileNotFoundException(Constants.wrongFileUpload + "" + ext);
                }
            }
            else
            {
                throw new FileNotFoundException(Constants.MeasurementFileMissing);
            }

            return new Tuple<string, string>(fileUrl, ext);
        }

        public static async Task<string> PostFile(byte[] fileByte, string ext)
        {
            string fileUrl = "";
            try
            {
                MemoryStream stream = new MemoryStream(fileByte);
                if (ext.Contains('/'))
                {
                    ext = ext.Split('/')[1];
                    ext = ext.Contains('.') ? ext.Split('.')[1] : ext;
                }

                fileUrl = Guid.NewGuid() + "." + ext;
                IFormFile blob = new FormFile(stream, 0, fileByte.Length, "azure", fileUrl)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = ext
                };
                await blobManager.PostAsync(new Blob { File = blob });
            }
            catch (Exception e)
            {
                SentrySdk.CaptureMessage(e.Message);
            }

            return fileUrl;
        }

        public static async Task<byte[]> GetFile(string filename)
        {
            byte[] image = await blobManager.Read(filename);
            return image;
        }

        public static async Task<string> DeleteFileFromBlob(string filename)
        {
            string status = "";
            if (filename != null)
            {
                await blobManager.Delete(filename);
                status = "Deleted";
            }
            else
            {
                status = "File Not Found";
            }

            return status;
        }

        public static async Task<object> testDownload(long clipId)
        {
            VimeoDotNet.Models.Video video = new VimeoDotNet.Models.Video();
            ServicePointManager.Expect100Continue = true;
            var vimeoClient = new VimeoClientFactory().GetVimeoClient(Constants.VimeoAccessToken);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var dvideo = vimeoClient.GetVideoAsync(clipId);
            
            return dvideo;
        }

        public static async Task<string> UploadUpdateVideo(byte[] file)
        {
            string fileUrl = "";
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                // fileUrl = Guid.NewGuid().ToString() + "." + exet;

                VimeoClient vimeoClient = new VimeoClient(Constants.VimeoAccessToken);
                BinaryContent binaryContent = new BinaryContent(file, "video/mp4");
                VimeoDotNet.Models.User authcheck = await vimeoClient.GetAccountInformationAsync();

                if (authcheck.Name != null)
                {
                    //IUploadRequest uploadRequest = new UploadRequest();
                    int chunksize = 0;
                    int contentlength = file.Length;
                    // int temp = contentlength / 1024;
                    if (contentlength > 1048576)
                    {
                        chunksize = contentlength / 10;
                    }
                    //chunksize = chunksize / 10;
                    // chunksize = chunksize * 1048576;
                    else
                    {
                        chunksize = 1048576;
                    }

                    
                    IUploadRequest uploadRequest = await vimeoClient.UploadEntireFileAsync(binaryContent, chunksize);
                    fileUrl = uploadRequest.ClipId.ToString();
                }
            }
            catch (Exception e)
            {
                SentrySdk.CaptureMessage(e.Message);
            }

            return fileUrl;
        }

        public static VideoDownloadURL GetVimeoVideoDownloadURL(String VideoID)
        {
            // First we build the Vimeo URL...
            String VimeoURL = "http://vimeo.com/" + VideoID;

            // now we retrieve that page...

            var VimeoVideoPage = string.Empty;
            using (var webClient = new System.Net.WebClient())
            {
                webClient.Headers.Add("user-agent", "Mozilla/5.0");
                VimeoVideoPage = webClient.DownloadString(VimeoURL);
            }

            if (VimeoVideoPage == string.Empty)
            {
                return null;
            }
            else
            {
                // we have the page and now we need to find the video configuration in there...

                // Convert the VideoPage into it's lines
                string[] VimeoVideoPageLines = VimeoVideoPage.Split('\n');

                var data_config_url_line = string.Empty;

                // Grep through the Lines and find the one with "data-config-url"
                foreach (String _line in VimeoVideoPageLines)
                {

                    if (_line.Contains("data-config-url"))
                    {
                        data_config_url_line = _line;
                        break;
                    }
                }

                // first find the data-config-url
                String ConfigURL = data_config_url_line.Remove(0, data_config_url_line.IndexOf("data-config-url=") + 17);
                ConfigURL = WebUtility.HtmlDecode(ConfigURL.Remove(ConfigURL.IndexOf("\""), ConfigURL.Length - ConfigURL.IndexOf("\"")));

                // Retrieve the configuration now
                var VimeoVideoConfiguration = string.Empty;
                using (var webClient = new System.Net.WebClient())
                {
                    webClient.Headers.Add("user-agent", "Mozilla/5.0");
                    VimeoVideoConfiguration = webClient.DownloadString(ConfigURL);
                }

                if (VimeoVideoConfiguration == string.Empty)
                {
                    return null;
                }
                else
                {

                    VimeoVideoConfigurationRootObject VimeoVideoConfigurationRoot = JsonConvert.DeserializeObject<VimeoVideoConfigurationRootObject>(WebUtility.HtmlDecode(VimeoVideoConfiguration));

                    return new VideoDownloadURL(VimeoVideoConfigurationRoot.request.files.h264.hd.url, VimeoVideoConfigurationRoot.video.title);
                }
            }
            return null;
        }

        public static async Task<string> DeleteVideo(long VideoId = 0)
        {
            string status = "";
            try
            {
                if (VideoId > 0)
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    VimeoClient vimeoClient = new VimeoClient(Constants.VimeoAccessToken);
                    await vimeoClient.DeleteVideoAsync(VideoId);

                    status = "Deleted";
                }
            }
            catch (Exception e)
            {
                SentrySdk.CaptureMessage(e.Message);
                status = e.Message;
            }

            return status;
        }

        public static string GuessFileType(byte[] file)
        {
            string f = Convert.ToBase64String(file);
            string s = f.Substring(0, 5);
            switch (s.ToUpper())
            {
                case "IVBOR":
                    return "png";
                case "/9J/2":
                case "/9J/4":
                    return "jpg";
                case "AAAAF":
                case "AAAAI":
                    return "mp4";
                case "JVBER":
                    return "pdf";
                case "AAABA":
                    return "ico";
                case "UMFYI":
                    return "rar";
                case "E1XYD":
                    return "rtf";
                case "U1PKC":
                    return "txt";
                case "MQOWM":
                case "77U/M":
                    return "srt";
                case "QUMXM":
                    return "dwg";
                default:
                    return "";
            }
        }

        public static void Each<T>(IEnumerable<T> items, Action<T> action)
        {
            foreach (T item in items)
            {
                action(item);
            }
        }

        public static string AddPayment(decimal? amount)
        {
            try
            {
                amount *= 100;
                PaymentIntentService paymentIntents = new PaymentIntentService();
                PaymentIntent paymentIntent = paymentIntents.Create(new PaymentIntentCreateOptions
                {
                    Amount = (long?)amount,
                    Currency = "aed"
                });
                return paymentIntent.ClientSecret;
            }
            catch (Exception e)
            {
                SentrySdk.CaptureMessage(e.Message);
            }

            return "";
        }

        public static void GenerateInvoice()
        {
            FileStream fileStream =
                new FileStream(@"D:\SAI Kitchen\BackendSaiKitchen\BackendSaiKitchen\Assets\invoice\invoice.docx",
                    FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            XWPFDocument wordDocument = new XWPFDocument(fileStream);

            //for (int i = 0; i < wordDocument.BodyElements.Count; i++)
            //{
            //    wordDocument.BodyElements[i].Body.Tables[0]a.ReplaceText("[invoiceCode]", "INV000");
            //    wordDocument.BodyElements[i].ReplaceText("[createdDate]", "14/08/2021");
            //    wordDocument.BodyElements[i].ReplaceText("[CustomerName]", "Sameer".ToUpper());
            //    wordDocument.BodyElements[i].ReplaceText("[CustomerEmail]", "sameer71095@gmail.com");
            //    wordDocument.BodyElements[i].ReplaceText("[CustomerContact]", "971545552471");
            //    wordDocument.BodyElements[i].ReplaceText("[CustomerAddress]", "Villa 50, Al Khamaari St Jumeira 3, Dubai");
            //    wordDocument.BodyElements[i].ReplaceText("[isPaid]", "");
            //    wordDocument.BodyElements[i].ReplaceText("[inquiryCode]", "IN111332");
            //    wordDocument.BodyElements[i].ReplaceText("[WorkScopeName]", "Kitchen");
            //    wordDocument.BodyElements[i].ReplaceText("[Amount]", "1000");
            //    wordDocument.BodyElements[i].ReplaceText("[Discount]", "15");
            //    wordDocument.BodyElements[i].ReplaceText("[TaxAmount]", "5");
            //    wordDocument.BodyElements[i].ReplaceText("[TotalAmount]", "850");
            //    wordDocument.BodyElements[i].ReplaceText("[MeasurementFee]", "250");
            //    wordDocument.BodyElements[i].ReplaceText("[PaymentAmount]", "200");
            //    wordDocument.BodyElements[i].ReplaceText("[EnglishAmount]", "Two hundred only");

            //    //paragraphs.Add(word);
            //    //paragraphs .Add= word.Text.Replace("[CustomerName]", "Sameer");
            //}
            for (int i = 0; i < wordDocument.Paragraphs.Count; i++)
            {
                wordDocument.Paragraphs[i].ReplaceText("[invoiceCode]", "INV000");
                wordDocument.Paragraphs[i].ReplaceText("[createdDate]", "14/08/2021");
                wordDocument.Paragraphs[i].ReplaceText("[CustomerName]", "Sameer".ToUpper());
                wordDocument.Paragraphs[i].ReplaceText("[CustomerEmail]", "sameer71095@gmail.com");
                wordDocument.Paragraphs[i].ReplaceText("[CustomerContact]", "971545552471");
                wordDocument.Paragraphs[i]
                    .ReplaceText("[CustomerAddress]", "Villa 50, Al Khamaari St Jumeira 3, Dubai");
                wordDocument.Paragraphs[i].ReplaceText("[isPaid]", "");
                wordDocument.Paragraphs[i].ReplaceText("[inquiryCode]", "IN111332");
                wordDocument.Paragraphs[i].ReplaceText("[WorkScopeName]", "Kitchen");
                wordDocument.Paragraphs[i].ReplaceText("[Amount]", "1000");
                wordDocument.Paragraphs[i].ReplaceText("[Discount]", "15");
                wordDocument.Paragraphs[i].ReplaceText("[TaxAmount]", "5");
                wordDocument.Paragraphs[i].ReplaceText("[TotalAmount]", "850");
                wordDocument.Paragraphs[i].ReplaceText("[MeasurementFee]", "250");
                wordDocument.Paragraphs[i].ReplaceText("[PaymentAmount]", "200");
                wordDocument.Paragraphs[i].ReplaceText("[EnglishAmount]", "Two hundred only");

                //paragraphs.Add(word);
                //paragraphs .Add= word.Text.Replace("[CustomerName]", "Sameer");
            }

            using (FileStream file =
                new FileStream(@"D:\SAI Kitchen\BackendSaiKitchen\BackendSaiKitchen\Assets\invoice\invoice2.docx",
                    FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                wordDocument.Write(file);
                file.Close();
            }

            //XWPFDocument doc = new XWPFDocument();
            //doc.CreateParagraph();
            //using (FileStream sw = System.IO.File.OpenWrite(path: @"D:\SAI Kitchen\BackendSaiKitchen\BackendSaiKitchen\Assets\invoice\invoice.docx"))
            //{
            //  foreach(var v in doc.Paragraphs)
            //    {

            //    }
            //doc.Write(sw);
            //}
        }

        public static async Task<string> SendWhatsappMessage(string sendto,string type , string message)
        {
            try
            {
                HttpClient client = new HttpClient();
                
                var values = new Dictionary<string, string>
            {
                {"number",sendto },
                {"type",type },
                {"message",message },
                {"instance_id",Constants.WhatsappInstanceId },
                {"access_token",Constants.WhatsappAccessToken }
            };

                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync("https://socialmediapartner.net/api/send.php?number="+ sendto + "&type="+type+"&message="+ message + "&instance_id="+ Constants.WhatsappInstanceId + "&access_token="+ Constants.WhatsappAccessToken , content);

                var responseString = await response.Content.ReadAsStringAsync();

                return responseString;
            }
            catch (Exception e)
            {
                Sentry.SentrySdk.CaptureMessage(e.Message);
                return e.Message;
            }
            
        }
    }
}