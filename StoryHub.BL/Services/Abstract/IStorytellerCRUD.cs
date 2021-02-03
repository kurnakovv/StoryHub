using StoryHub.BL.Models;
using System;
using System.Threading.Tasks;

namespace StoryHub.BL.Services.Abstract
{
    public interface IStorytellerCRUD
    {
        Task<string> CreateStoryteller(Storyteller storyteller);
        Storyteller GetStorytellerById(string id);
        Task<Storyteller> UpdateStoryteller(Storyteller storyteller);
        Task DeleteStorytellerById(string id);
    }
}
