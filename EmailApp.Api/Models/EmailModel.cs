using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmailApp.Api.Models
{
    public class EmailModel
    {
        //TODO: More validation required

        public string Subject { get; set; }

        [Required]
        public string[] To { get; set; }

        public string[] Cc { get; set; }

        public string[] Bcc { get; set; }

        public string Message { get; set; }

        public int Id { get; set; }
    }
}