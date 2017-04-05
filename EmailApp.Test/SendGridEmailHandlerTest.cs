using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using EmailApp.Api.Handlers;
using EmailApp.Api.Models;

namespace EmailApp.Test
{
    public class SendGridEmailHandlerTest
    {
        /// <summary>
        /// Test the email model mapping
        /// </summary>
        [Test]
        public void TestMapToModel()
        {
            var sgHandler = new SendGridHandler(new MailGunHandler());
            const string subject = "Test Subject";
            const string message = "Test Message.";
            const string toAddress = "sgTest@sgTest.com";

            var email = new EmailModel()
            {
                Message = message,
                To = new string[] { toAddress },
                Subject = subject,
            };

            var mapped = sgHandler.mapToSendGridEmail(email);

            Assert.NotNull(mapped);
            Assert.AreEqual(subject, mapped.subject);

            Assert.IsTrue(mapped.content.Count == 1);
            Assert.AreEqual(message, mapped.content[0].value);

            Assert.IsTrue(mapped.personalizations.Count == 1);
            Assert.IsTrue(mapped.personalizations[0].to.Count == 1);
            Assert.AreEqual(toAddress, mapped.personalizations[0].to[0].email);
        }

        /// <summary>
        /// Test send grid email sending
        /// </summary>
        [Test]
        public void TestSendEmail()
        {
            var sgHandler = new SendGridHandler(new MailGunHandler());
            const string subject = "Test Subject";
            const string message = "Test Message.";
            const string toAddress = "athuraliy@gmail.com";

            var email = new EmailModel()
            {
                Message = message,
                To = new string[] { toAddress },
                Subject = subject,
            };

            sgHandler.SendEmail(email);
        }
    }
}
