using StoryHub.BL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoryHub.BL.Services.Abstract
{
    public interface IStorytellerService
    {
        Task<IEnumerable<Storyteller>> GetAllStorytellers();
        Storyteller FindStorytellerById(string id);
        IEnumerable<Storyteller> FindStorytellersByUserName(string userName);
        void AddSubscriber();
    }
}
