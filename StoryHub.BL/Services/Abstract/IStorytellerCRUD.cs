using StoryHub.BL.Models;
using System;

namespace StoryHub.BL.Services.Abstract
{
    public interface IStorytellerCRUD
    {
        Guid CreateStoryteller(Storyteller storyteller);
        Storyteller GetStorytellerById(Guid id);
        Storyteller UpdateStoryteller(Storyteller storyteller);
        void DeleteStorytellerById(Guid id);
    }
}
