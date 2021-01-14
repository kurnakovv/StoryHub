using System;
using System.ComponentModel.DataAnnotations;

namespace StoryHub.BL.Models
{
    public class StoryCategory
    {
        [Key]
        public Guid Id { get; private set; }
        public DateTime DateOfCreate { get; private set; }
        public string Name { get; private set; }

        public StoryCategory() 
        {
            Id = Guid.NewGuid();
            DateOfCreate = DateTime.Now;
        }
        public StoryCategory(string name)
        {
            Name = name;
        }
    }
}
