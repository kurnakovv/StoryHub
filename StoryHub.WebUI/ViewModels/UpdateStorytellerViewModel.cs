using System.ComponentModel.DataAnnotations;

namespace StoryHub.WebUI.ViewModels
{
    public class UpdateStorytellerViewModel
    {
        [Required]
        public string Id { get; set; }
        [Display(Name = "Image is valid?")]
        public bool IsValidImage { get; set; }
        [Required]
        public string About { get; set; }
        [Display(Name = "Image")]
        public string OldStorytellerImage { get; set; }
    }
}
