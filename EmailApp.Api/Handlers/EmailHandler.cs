using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailApp.Api.Handlers
{
    public abstract class EmailHandler
    {
        protected EmailHandler _successor;

        public void SetSuccessor(EmailHandler successor)
        {
            _successor = successor;
        }

        public abstract void SendEmail(Models.EmailModel email);
    }
}