using System;
using System.Globalization;
using System.Linq;

namespace BackendSaiKitchen.Helper
{
    public class Helper
    {
        static CultureInfo provider = CultureInfo.InvariantCulture;
        public static String GenerateToken(int userId)
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            byte[] id = System.Text.Encoding.ASCII.GetBytes(userId.ToString());
            return Convert.ToBase64String(id.Concat(time.Concat(key)).ToArray());
        }
        public static String GetDateTime()
        {
            //04 / 27 / 2021 10:01 AM
            return DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
        }
        public static DateTime ConvertToDateTime(String dateTime)
        {
            //04 / 27 / 2021 10:01 AM
            DateTime dateTimeParsed; 
             DateTime.TryParseExact(dateTime, new string[] { "MM/dd/yyyy hh:mm tt", "MM/dd/yyyy h:mm tt" },provider, DateTimeStyles.None, out dateTimeParsed);
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
    }
}
