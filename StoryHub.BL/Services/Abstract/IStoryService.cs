using StoryHub.BL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoryHub.BL.Services.Abstract
{
    public interface IStoryService
    {
        Task<IEnumerable<Story>> GetAllStories();
        Task<IEnumerable<Story>> GetStorytellerStories(Storyteller storyteller);
    }
}
