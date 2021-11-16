using BackendSaiKitchen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.CustomModel
{

    public class NewsletterCustom
    {
        public int NewsletterId { get; set; }
        public string NewsletterHeading { get; set; }
        public string NewsletterSendingDate { get; set; }
        public int NewsletterTypeId { get; set; }
        public int NewsletterFrequencyId { get; set; }
        public string NewsletterBody { get; set; }
        public string NewsletterAttachmentUrl { get; set; }
    }

    public class Isactive
    {
        public int NewsletterId { get; set; }
        public bool isactive { get; set; }
    }
    public class NewsletterTypeCustom
    {
        public int NewsletterTypeId { get; set; }
        public string NewsletterTypeDescription { get; set; }
        public string NewsletterTypeName { get; set; }

    }
}
