using System.ComponentModel.DataAnnotations;

namespace MeriDiaryv2.Models
{
    public class DiaryEntry
    {
        public int Id { get; set; }  // Primary key

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Mood is required.")]
        [MaxLength(20, ErrorMessage = "Mood cannot exceed 20 characters.")]
        public string Mood { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;  // Auto-set to current date

        // Foreign Key
        public int UserId { get; set; }
        public User User { get; set; }  // Navigation property
    }
}
