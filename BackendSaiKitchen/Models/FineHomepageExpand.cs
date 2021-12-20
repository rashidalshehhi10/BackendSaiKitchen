using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineHomepageExpand
    {
        public string Id { get; set; }
        public string AndroidPadHomePage { get; set; }
        public string AndroidPhoneHomePage { get; set; }
        public string IPadHomePage { get; set; }
        public string IPhoneHomePage { get; set; }
        public string PcHomePage { get; set; }
        public int? Type { get; set; }
    }
}
