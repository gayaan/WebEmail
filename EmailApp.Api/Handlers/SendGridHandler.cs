using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmailApp.Api.Models;
using EmailApp.Api.WebMail;
using System.Net.Http;

namespace EmailApp.Api.Handlers
{
    public class SendGridHandler : EmailHandler
    {
        //TODO: better to setup in config
        private readonly string _url = "https://api.sendgrid.com/v3/mail";

        public SendGridHandler(EmailHandler successor)
        {
            _successor = successor;
        }

        /// <summary>
        /// Sends an email 
        /// </summary>
        /// <param name="email">email data</param>
        public override void SendEmail(EmailModel email)
        {
            //If send grid fails and successor is available delegate to the successor
            if (!Send(email) && _successor != null)
            {
                _successor.SendEmail(email);
            }
        }

        private bool Send(EmailModel email)
        {
            using (var client = new HttpClient())
            {
                var mappedRequestData = mapToSendGridEmail(email);

                //Better to get from config 
                client.DefaultRequestHeaders.Add("Authorization", "Bearer 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");

                var response = client.PostAsJsonAsync<SendGridEmail>(string.Format("{0}/send", _url), mappedRequestData).Result;

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Maps the email model to send grid specific request model
        /// </summary>
        /// <param name="email">email model</param>
        /// <returns>SendGridEmail</returns>
        public SendGridEmail mapToSendGridEmail(EmailModel email)
        {
            //create the send grid specific model
            var sendGridEmail = new SendGridEmail()
            {
                subject = email.Subject,
                personalizations = new List<Personalization>(),
                content = new List<Content>()
            };

            var personalization = new Personalization() { 
                                    to = new List<Recipient>(),
                                    //cc = new List<Recipient>(),
                                    //bcc = new List<Recipient>()
                                };


            personalization.to.AddRange(email.To.Select(t => new Recipient { email = t }));
            //personalization.cc.AddRange(email.Cc.Select(t => new Recipient { email = t }));
            //personalization.bcc.AddRange(email.Bcc.Select(t => new Recipient { email = t }));

            sendGridEmail.personalizations.Add(personalization);
            
            var content = new Content() { type = "text/plain", value = email.Message };
            sendGridEmail.content.Add(content);

            //TODO: From address is hard coded. Place to config
            sendGridEmail.from = new From() { email = "athuraliy@gmail.com" };

            return sendGridEmail;
        }
    }
}