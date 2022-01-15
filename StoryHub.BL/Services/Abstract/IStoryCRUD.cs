using StoryHub.BL.Models;
using System.Threading.Tasks;

namespace StoryHub.BL.Services.Abstract
{
    public interface IStoryCRUD
    {
        Task<string> CreateStory(Story story);
        Story GetStoryById(string id);
        Task<Story> UpdateStory(Story story);
        Task DeleteStoryById(string id);
    }
}
