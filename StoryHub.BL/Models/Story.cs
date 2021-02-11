using System;
using System.ComponentModel.DataAnnotations;

namespace StoryHub.BL.Models
{
    public class Story
    {
        [Key]
        public string Id { get; private set; }
        [Required]
        [Display(Name = "Date of create.")]
        public DateTime DateOfCreate { get; private set; }
        [Required]
        public string Name { get; private set; }
        public string Image { get; private set; }
        [Required]
        public string Text { get; private set; }
        [Display(Name = "Quantity likes.")]
        public int QuantityLikes { get; set; }
        [Display(Name = "Quantity dislikes.")]
        public int QuantityDislikes { get; set; }
        public StoryCategory Category { get; private set; }
        public Storyteller Storyteller { get; private set; }

        public Story() { }
        public Story(string name,
                     string image,
                     string text,
                     int quantityLikes,
                     int quantityDislikes,
                     StoryCategory category,
                     Storyteller storyteller)
        {
            Id = Guid.NewGuid().ToString();
            DateOfCreate = DateTime.Now;
            Name = name;
            Image = image;
            Text = text;
            QuantityLikes = quantityLikes;
            QuantityDislikes = quantityDislikes;
            Category = category;
            Storyteller = storyteller;
        }
    }
}
