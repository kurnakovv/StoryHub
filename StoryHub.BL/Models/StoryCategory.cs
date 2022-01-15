using System;
using System.ComponentModel.DataAnnotations;

namespace StoryHub.BL.Models
{
    public class StoryCategory
    {
        [Key]
        public string Id { get; private set; }
        public DateTime DateOfCreate { get; private set; }
        public string Name { get; private set; }

        public StoryCategory(string name)
        {
            Id = Guid.NewGuid().ToString();
            DateOfCreate = DateTime.Now;
            Name = name;
        }
    }
}
