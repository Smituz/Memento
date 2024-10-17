using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeriDiaryv2.Models
{
    public class User
    {
        public int Id { get; set; }  // Primary key

        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNo { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Birthdate is required.")]
        public DateTime BirthDate { get; set; }

        // Navigation Properties
        public ICollection<DiaryEntry> DiaryEntries { get; set; }
        public ICollection<Goal> Goals { get; set; }
    }
}
