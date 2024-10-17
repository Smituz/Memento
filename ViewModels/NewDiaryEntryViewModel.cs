using System.ComponentModel.DataAnnotations;

namespace MeriDiaryv2.ViewModels
{
    public class NewDiaryEntryViewModel
    {
        public int Id { get; set; } // Add ID field

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Please select a mood.")]
        public string Mood { get; set; }
    }
}
