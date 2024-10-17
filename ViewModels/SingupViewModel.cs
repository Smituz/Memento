using System.ComponentModel.DataAnnotations;

namespace MeriDiaryv2.ViewModels
{
    public class SingupViewModel
    {
            [Required(ErrorMessage = "First Name is required.")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "Last Name is required.")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "Phone Number is required.")]
            [Phone(ErrorMessage = "Invalid phone number.")]
            public string PhoneNo { get; set; }

            [Required(ErrorMessage = "Email is required.")]
            [EmailAddress(ErrorMessage = "Invalid email address.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Password is required.")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Passwords do not match.")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "Birthdate is required.")]
            [DataType(DataType.Date)]
            public DateTime Birthdate { get; set; }


    }
}
