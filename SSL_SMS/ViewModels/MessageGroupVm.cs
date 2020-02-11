using SSL_SMS.DAL;
using SSL_SMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SSL_SMS.ViewModels
{
    public class MessageGroupVm
    {
        [Display(Name = "Message Group")]
        public virtual IEnumerable<MessageGroupDto> MessageGroups { get; set; }

        [Display(Name = "Contact Group")]
        public virtual IEnumerable<ContactGroupDto> ContactGroups { get; set; }

        [Display(Name = "Contacts")]
        public string Contacts { get; set; }

        [Display(Name = "Messages")]
        public string Messages { get; set; }

        public SendSmsStatu SendSmsStatu { get; set; }
    }
}