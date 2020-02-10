using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SSL_SMS.Models
{
    public class MessageGroupDto
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Group Name")]
        public string GroupName { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string Message { get; set; }

        [Display(Name = "Create User")]
        public string Create_User { get; set; }

        [Display(Name = "Create Date")]
        public Nullable<System.DateTime> Create_Date { get; set; }

        [Display(Name = "Edit User")]
        public string Edit_User { get; set; }

        [Display(Name = "Edit Date")]
        public Nullable<System.DateTime> Edit_Date { get; set; }
    }
}