using StoryHub.BL.Models;
using System;

namespace StoryHub.BL.Services.Abstract
{
    public interface IStorytellerCRUD
    {
        string CreateStoryteller(Storyteller storyteller);
        Storyteller GetStorytellerById(string id);
        Storyteller UpdateStoryteller(Storyteller storyteller);
        void DeleteStorytellerById(string id);
    }
}
