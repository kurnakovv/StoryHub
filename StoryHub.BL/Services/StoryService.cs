using Microsoft.EntityFrameworkCore;
using StoryHub.BL.Db_Context;
using StoryHub.BL.Models;
using StoryHub.BL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoryHub.BL.Services
{
    public class StoryService : IStoryService, IStoryCRUD
    {
        private readonly AppDbContext _appDbContext = new AppDbContext();

        public async Task<string> CreateStory(Story story)
        {
            if (_appDbContext.Stories.Contains(story))
                throw new Exception("The story is has already");

            _appDbContext.Stories.Add(story);
            await _appDbContext.SaveChangesAsync();
            await _appDbContext.DisposeAsync();
            return story.Id;
        }

        public async Task DeleteStoryById(string id)
        {
            var story = GetStoryById(id);
            _appDbContext.Stories.Remove(story);
            await _appDbContext.SaveChangesAsync();
            await _appDbContext.DisposeAsync();
        }

        public async Task<IEnumerable<Story>> GetAllStories()
        {
            return await _appDbContext.Stories.ToListAsync();
        }

        public async Task<IEnumerable<Story>> GetStorytellerStories(Storyteller storyteller)
        {
            IEnumerable<Story> stories = await _appDbContext.Stories
                            .Include(story => story.Storyteller)
                            .Where(story => story.StorytellerId == storyteller.Id)
                            .ToListAsync();

            if(stories.Count() == 0)
            {
                throw new InvalidOperationException("You have not stories.");
            }

            return stories;
        }

        public Story GetStoryById(string id)
        {
            if (id == null)
                throw new InvalidOperationException($"Id cannot be empty.");

            var result = _appDbContext.Stories.Find(id);

            if (result == null)
                throw new KeyNotFoundException($"Expected record for id {id} not found.");

            return result;
        }

        public async Task<Story> UpdateStory(Story story)
        {
            var oldStory = GetStoryById(story.Id);
            _appDbContext.Entry(oldStory).CurrentValues.SetValues(story);
            await _appDbContext.SaveChangesAsync();
            await _appDbContext.DisposeAsync();
            return story;
        }
    }
}
