using StoryHub.BL.Models;
using System;
using System.Collections.Generic;

namespace StoryHub.BL.Services.Abstract
{
    public interface IStorytellerService
    {
        IEnumerable<Storyteller> GetAllStorytellers();
        Storyteller FindStorytellerById(Guid id);
        void AddSubscriber();
    }
}
