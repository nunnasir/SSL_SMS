using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SSL_SMS.Models
{
    public class GroupDto
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Group Name")]
        public string Name { get; set; }

        [Display(Name = "Create User")]
        public string CreateUser { get; set; }

        [Display(Name = "Create Date")]
        public Nullable<DateTime> CreateDate { get; set; }

        [Display(Name = "Update User")]
        public string UpdateUser { get; set; }

        [Display(Name = "Update Date")]
        public Nullable<DateTime> UpdateDate { get; set; }

        [Display(Name = "Total Contacts")]
        public virtual ICollection<ContactDto> Contacts { get; set; }
    }
}