using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SSL_SMS.Models
{
    public class ContactDto
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Group Name")]
        public Nullable<int> GroupId { get; set; }

        [Required]
        [Display(Name = "Contact List")]
        public string ContactList { get; set; }

        [Display(Name = "Create User")]
        public string CreateUser { get; set; }

        [Display(Name = "Create Date")]
        public Nullable<System.DateTime> CreateDate { get; set; }

        [Display(Name = "Update User")]
        public string UpdateUser { get; set; }

        [Display(Name = "Update Date")]
        public Nullable<System.DateTime> UpdateDate { get; set; }

        public virtual GroupDto Group { get; set; }

        //public List<string> InvalidNumber { get; set; }
        //public List<string> DuplicateNumber { get; set; }
        //public int validNumber { get; set; }
    }
}