using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailApp.Api.WebMail
{
    public class SendGridEmail
    {
        public List<Personalization> personalizations { get; set; }
        public From from { get; set; }
        public string subject { get; set; }
        public List<Content> content { get; set; }  
    }

    public class Recipient
    {
        public string email { get; set; }
    }

    public class Personalization
    {
        public List<Recipient> to { get; set; }

        //public List<Recipient> cc { get; set; }

        //public List<Recipient> bcc { get; set; }
    }

    public class From
    {
        public string email { get; set; }
    }

    public class Content
    {
        public string type { get; set; }
        public string value { get; set; }
    }
}