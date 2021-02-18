using System.ComponentModel.DataAnnotations;

namespace StoryHub.WebUI.ViewModels
{
    public class StoryViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
