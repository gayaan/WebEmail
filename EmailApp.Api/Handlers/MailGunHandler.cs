using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace EmailApp.Api.Handlers
{
    public class MailGunHandler : EmailHandler
    {
        //TODO: better to setup in config
        private readonly string _url = "https://api.mailgun.net/v3";

        public override void SendEmail(Models.EmailModel email)
        {
            //TODO: Implement the email send request to mail gun
            using (var client = new HttpClient())
            {
 
            }

            throw new NotImplementedException();
        }
    }
}