using System.ComponentModel.DataAnnotations;

namespace MeriDiaryv2.Models
{
    public class Goal
    {
        public int Id { get; set; }  // Primary key

        [Required(ErrorMessage = "Goal title is required.")]
        [MaxLength(100, ErrorMessage = "Goal title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Deadline is required.")]
        public DateTime Deadline { get; set; }  // After this date, the goal will be hidden

        // Foreign Key
        public int UserId { get; set; }
        public User User { get; set; }  // Navigation property
    }
}
