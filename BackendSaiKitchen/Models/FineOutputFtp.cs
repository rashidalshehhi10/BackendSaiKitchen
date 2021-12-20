using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineOutputFtp
    {
        public string Id { get; set; }
        public string FtpMode { get; set; }
        public string Password { get; set; }
        public string Port { get; set; }
        public string SavePath { get; set; }
        public string ServerAddress { get; set; }
        public string Username { get; set; }
    }
}
