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

        
         public List<byte[]> Base64img { get; set; }
    }
}
