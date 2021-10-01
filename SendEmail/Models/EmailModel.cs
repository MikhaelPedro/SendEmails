using OpenXmlPowerTools;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SendEmail.Models
{
    public class EmailModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Sucess")]
        public bool Sucess { get; set; }

        //public void PasswordMethod()
        //{
        //    //string password = "";
            
        //    return password.ToString();
        //}



    }
}