using System;
using System.ComponentModel.DataAnnotations;

namespace StoryHub.BL.Models
{
    public class Story
    {
        [Key]
        public Guid Id { get; private set; }
        public DateTime DateOfCreate { get; private set; }
        public string Name { get; private set; }
        public string Image { get; private set; }
        public string Text { get; private set; }
        public int QuantityLikes { get; private set; }
        public int QuantityDislikes { get; private set; }
        public StoryCategory Category { get; private set; }
        public Storyteller Storyteller { get; private set; }

        public Story() 
        {
            Id = Guid.NewGuid();
            DateOfCreate = DateTime.Now;
        }
        public Story(string name,
                     string image,
                     string text,
                     int quantityLikes,
                     int quantityDislikes,
                     StoryCategory category,
                     Storyteller storyteller)
        {
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
