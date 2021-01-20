using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoryHub.BL.Models
{
    public class Storyteller : IdentityUser
    {
        [Required(ErrorMessage = "Enter your name!")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "The name cannot be more than 30 characters")]
        public string Name { get; private set; }

        [Required]
        [Range(0, int.MaxValue)]
        [DefaultValue(0)]
        public int Folowers { get; private set; }

        [Required]
        [Range(0, int.MaxValue)]
        [DefaultValue(0)]
        public int QuantityStories { get; private set; }

        [DefaultValue(false)]
        public bool Gender { get; private set; }

        [DefaultValue("")] // TODO: Add default img
        public string Image { get; private set; }

        [StringLength(50, ErrorMessage = "Information about you cannot be more than 30 characters")]
        public string About { get; private set; }

        [Range(1, 100, ErrorMessage = "Enter valid age (1 - 100)")]
        public int Age { get; private set; }

        public Storyteller() { }
        public Storyteller(string name,
                           int quantityStories,
                           bool gender,
                           string image,
                           string about,
                           int age)
        {
            Name = name;
            QuantityStories = quantityStories;
            Gender = gender;
            Image = image;
            About = about;
            Age = age;
        }

        public void AddSubscriber()
        {
            this.Folowers++;
        }
    }
}
