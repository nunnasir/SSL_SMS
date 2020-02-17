using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SSL_SMS.ViewModels
{
    public class LoginVm
    {
        [Required]
        [Display(Name = "Username")]
        public string User_Name { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string User_Password { get; set; }

        public string ErrorMessage { get; set; }
    }
}