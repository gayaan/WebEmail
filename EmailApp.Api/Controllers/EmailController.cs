using EmailApp.Api.Handlers;
using EmailApp.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmailApp.Api.Controllers
{
    public class EmailController : ApiController
    {
        private EmailHandler _emailHandler;
        
        //Constructor to used with Dependency Injection
        public EmailController(EmailHandler emailHandler)
        {
            _emailHandler = emailHandler;
        }

        //Empty Constructor
        public EmailController()
        {

        }

        // POST api/email 
        public HttpResponseMessage Post(EmailModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid request");
            }

            try
            {
                _emailHandler.SendEmail(model);
            }
            catch (Exception ex)
            {
                //TODO: Log error

                var resp = new HttpResponseMessage(HttpStatusCode.OK)
                            {
                                Content = new StringContent("Unexpected Error"),
                                ReasonPhrase = "Unexpected Error."
                            };

                throw new HttpResponseException(resp);
            }

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }
    }
}
