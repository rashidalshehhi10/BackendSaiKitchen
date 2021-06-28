using System.Collections.Generic;

namespace BackendSaiKitchen.CustomModel
{
    public class CustomMeasFiles
    {
        public int Ininquiryworkscopeid { get; set; }
        public string measurementComment { get; set; }


        public List<byte[]> base64img { get; set; }
        public List<byte[]> videobase64 { get; set; }
    }
}
