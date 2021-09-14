using System.Collections.Generic;

namespace BackendSaiKitchen.CustomModel
{
    public class CustomMeasFiles
    {
        public int inquiryid { get; set; }
        public string measurementComment { get; set; }


        public List<string> base64img { get; set; }
        //public List<string> videobase64 { get; set; }
    }
}
