using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineInternational
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string I18nKey { get; set; }
        public string Language { get; set; }
        public string I18nValue { get; set; }
    }
}
