using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoryHub.BL.Db_Context;
using StoryHub.BL.Models;
using StoryHub.BL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StoryHub.BL.Services
{
    public class StorytellerService : IStorytellerService, IStorytellerCRUD
    {
        private readonly AppDbContext _appDbContext = new AppDbContext();
        private readonly UserManager<Storyteller> _userManager;

        public StorytellerService() { }
        public StorytellerService(UserManager<Storyteller> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<Storyteller>> GetAllStorytellers() 
            => await _appDbContext.Storytellers.ToListAsync();

        public async Task<string> CreateStoryteller(Storyteller storyteller)
        {
            if (_appDbContext.Storytellers.Contains(storyteller))
                throw new Exception("The entity is has already");


            _appDbContext.Storytellers.Add(storyteller);
            await Save();
            return storyteller.Id;
        }

        public async Task DeleteStorytellerById(string id)
        {
            if (id != null)
            {
                Storyteller storyteller = FindStorytellerById(id);
                if (storyteller != null)
                {
                    _appDbContext.Storytellers.Remove(storyteller);
                    await Save();
                }
            }
        }

        public Storyteller GetStorytellerById(string id)
        {
            if (id != null)
            {
                Storyteller storyteller = FindStorytellerById(id);
                return storyteller;
            }

            throw new KeyNotFoundException("Storyteller not found by id");
        }

        public async Task<Storyteller> GetCurrentStoryteller(ClaimsPrincipal currentStoryteller)
        {
            if(currentStoryteller == null)
            {
                throw new InvalidOperationException("Storyteller cannot be empty.");
            }

            return await _userManager.GetUserAsync(currentStoryteller);
        }

        public async Task<Storyteller> UpdateStoryteller(Storyteller storyteller)
        {
            if (storyteller != null)
            {
                var entry = FindStorytellerById(storyteller.Id);
                if (entry != null)
                {
                    _appDbContext.Entry(entry).CurrentValues.SetValues(storyteller);
                    await Save();
                    return storyteller;
                }
            }

            throw new Exception("Storyteller cannot be empty");
        }

        

        public Storyteller FindStorytellerById(string id) => _appDbContext.Storytellers.Find(id);
        public IEnumerable<Storyteller> FindStorytellersByUserName(string userName)
        {
            IEnumerable<Storyteller> storytellers = 
                _appDbContext.Storytellers.Where(s => s.UserName.Contains(userName));

            if (storytellers.Count() == 0)
                throw new Exception("Storyteller not found.");

            return storytellers;
        }

        public void AddSubscriber() { }

        private async Task Save()
        {
            await _appDbContext.SaveChangesAsync();
            await _appDbContext.DisposeAsync();
        }
    }
}

//if (storyteller.Id != null ||
//   storyteller.Image != null ||
//   storyteller.LockoutEnd != null ||
//   storyteller.Name != null ||
//   storyteller.NormalizedEmail != null ||
//   storyteller.NormalizedUserName != null ||
//   storyteller.PasswordHash != null ||
//   storyteller.PhoneNumber != null ||
//   storyteller.SecurityStamp != null ||
//   storyteller.UserName != null)
//{