using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
