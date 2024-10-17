using System;
using System.ComponentModel.DataAnnotations;

namespace MeriDiaryv2.ViewModels
{
    public class AddGoalViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Goal title is required.")]
        [MaxLength(100, ErrorMessage = "Goal title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Deadline is required.")]
        public DateTime Deadline { get; set; }
        public bool Completed { get; set; }

    }
}
