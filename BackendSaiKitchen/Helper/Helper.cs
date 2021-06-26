using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VimeoDotNet;
using VimeoDotNet.Net;

namespace BackendSaiKitchen.Helper
{
    public class Helper
    {
        static CultureInfo provider = CultureInfo.InvariantCulture;
        public static IBlobManager blobManager;


     static TimeZoneInfo UAETimeZone = TimeZoneInfo.FindSystemTimeZoneById("Arabian Standard Time");
        public static String GenerateToken(int userId)
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            byte[] id = System.Text.Encoding.ASCII.GetBytes(userId.ToString());
            return Convert.ToBase64String(id.Concat(time.Concat(key)).ToArray());
        }
        public static String GetDateTime()
        {
            DateTime utc = DateTime.UtcNow;
            DateTime UAE = TimeZoneInfo.ConvertTimeFromUtc(utc, UAETimeZone);
            //04 / 27 / 2021 10:01 AM
            return UAE.ToString("MM/dd/yyyy hh:mm tt");
        }
        public static DateTime ConvertToDateTime(String dateTime)
        {
            //04 / 27 / 2021 10:01 AM
            DateTime dateTimeParsed;
            DateTime.TryParseExact(dateTime, new string[] { "MM/dd/yyyy hh:mm tt", "MM/dd/yyyy h:mm tt" }, provider, DateTimeStyles.None, out dateTimeParsed);

            return dateTimeParsed;
        }
        public static String GetDateFromString(String dateTime)
        {
            //04 / 27 / 2021 10:01 AM
            if (dateTime != null)
            {
                return DateTime.ParseExact(dateTime, "MM/dd/yyyy hh:mm tt", null).ToString("MM/dd/yyyy");
            }
            else
            {
                return "";
            }
        }
        public static Object SetResponse(Object obj)
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
                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
            }
            catch (FormatException fe)
            {
                decrypted = "";
            }
            return decrypted;
        }

        public static string EnryptString(string strEncrypted)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }


        public static async Task<Tuple<string, string>> UploadFileToBlob(byte[] fileByte)
        {
            string fileUrl = "";
            string ext = "";
            if (fileByte != null)
            {
                MemoryStream stream = new MemoryStream(fileByte);
                ext = GuessFileType(fileByte);
                fileUrl = Guid.NewGuid().ToString() + "." + ext;
                IFormFile blob = new FormFile(stream, 0, fileByte.Length, "azure", fileUrl);
                if (ext == "png" || ext == "jpg" || ext == "pdf")
                {
                    await blobManager.Upload(new Blob() { File = blob });
                }
                else
                {
                    throw new FileNotFoundException(Constants.MeasurementFileMissing);
                }
            }
            else { 
                throw new FileNotFoundException(Constants.MeasurementFileMissing); 
            }
            return new Tuple<string, string>(fileUrl, ext);
        }

        public static async Task<Tuple<string, string>> UploadUpdateVideo(byte[] file)
        {
            string fileUrl="";
            string ext = "";
            try
            {
                if (file != null)
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    ext = GuessFileType(file);
                   // fileUrl = Guid.NewGuid().ToString() + "." + exet;
                    if (ext == "mp4")
                    {
                        VimeoClient vimeoClient = new VimeoClient(Constants.VimeoAccessToken);
                        BinaryContent binaryContent = new BinaryContent(file, "video/mp4");
                        var authcheck = await vimeoClient.GetAccountInformationAsync();

                        if (authcheck.Name != null)
                        {
                            IUploadRequest uploadRequest = new UploadRequest();
                            int chunksize =0;
                            int contentlength = file.Length;
                           // int temp = contentlength / 1024;
                            if (contentlength > 1048576)
                            {
                                chunksize = contentlength / 10;
                                //chunksize = chunksize / 10;
                               // chunksize = chunksize * 1048576;
                            }
                            else
                            {
                                chunksize = 1048576; 
                            }
                            
                            uploadRequest = await vimeoClient.UploadEntireFileAsync(binaryContent,chunksize, null);
                            fileUrl = uploadRequest.ClipId.ToString();
                            
                        }
                    }
                    else
                    {
                        throw new FileNotFoundException(Constants.DesignVideoFileMissing);
                    }
                }
                else
                {
                    throw new FileNotFoundException(Constants.DesignVideoFileMissing);
                }

            }
                catch (Exception e)
            {
                Sentry.SentrySdk.CaptureMessage(e.Message);
            }
            return new Tuple<string, string>(fileUrl, ext);
        }

        public static string GuessFileType(byte[] file)
        {
            string f = Convert.ToBase64String(file);
            string s = f.Substring(0, 5);
            switch (s.ToUpper())
            {
                case "IVBOR":
                    return "png";
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
                default:
                    return "";
            }
        }

        public static void Each<T>(IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
                action(item);
        }
    }
}
