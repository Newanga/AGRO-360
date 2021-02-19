using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Agro360.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(50,ErrorMessage ="First Name should be at least 2 characters",MinimumLength =2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "Last Name should be at least 2 characters", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "NIC")]
        [RegularExpression(@"^([0-9]{9}[x|X|v|V]|[0-9]{12})$", ErrorMessage = "Invalid NIC Format")]
        public string NIC { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Invalid Zip Format")]
        public int ZipCode { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{7,}$", ErrorMessage = "Enter strong Password")]
        [NotMapped]
        public string Psswd { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare(nameof(Psswd))]
        [NotMapped]
        public string ConfirmPsswd { get; set; }

        [Required]   
        [NotMapped]
        [RegularExpression(@"^\d{3}-\d{7}$", ErrorMessage = "Invalid Contact No")]
        public string ContactNo { get; set; }

       [Required]
       [NotMapped]
       [Display(Name = "Username")]
        public string uname { get; set; }

        [Required]
        [EmailAddress]
        [NotMapped]
        [Display(Name = "Email Address")]
        public string mail { get; set; }
    }
}
