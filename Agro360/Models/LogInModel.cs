using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agro360.Models
{
    public class LogInModel
    {
        [DisplayName("Email or Username")]
        [MinLength(3,ErrorMessage ="Invalid Email or Username")]
        [Required(ErrorMessage ="Email Or Username is Required")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Password")]
        [MinLength(7,ErrorMessage ="Invalid Password length")]
        [Required]
        public string Password { get; set; }

        [DisplayName("Remember me?")]
        public bool RememberMe { get; set; }
    }
}
