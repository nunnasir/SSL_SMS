using SSL_SMS.DAL;
using SSL_SMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SSL_SMS.ViewModels
{
    public class SmsSendVm
    {
        [Display(Name = "Contact Group")]
        public virtual IEnumerable<GroupDto> ContactGroups { get; set; }

        [Display(Name = "Contacts")]
        [Required]
        public string Contacts { get; set; }

        [Display(Name = "Messages")]
        [Required]
        public string Messages { get; set; }

        public SmsLog SmsLogs { get; set; }
    }
}