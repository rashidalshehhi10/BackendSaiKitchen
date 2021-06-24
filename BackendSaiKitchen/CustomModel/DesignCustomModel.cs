using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.CustomModel
{
    public class DesignCustomModel
    {
        public int inquiryWorkScopeId { get; set; }
        public List<byte[]> base64f3d { get; set; }
        public string comment { get; set; }
    }
}
