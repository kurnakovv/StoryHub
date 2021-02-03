using Microsoft.EntityFrameworkCore;
using StoryHub.BL.Db_Context;
using StoryHub.BL.Models;
using StoryHub.BL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoryHub.BL.Services
{
    public class StorytellerService : IStorytellerService, IStorytellerCRUD
    {
        private readonly AppDbContext _appDbContext = new AppDbContext();

        public IEnumerable<Storyteller> GetAllStorytellers() => _appDbContext.Storytellers.ToList();

        public string CreateStoryteller(Storyteller storyteller)
        {
            if (_appDbContext.Storytellers.Contains(storyteller))
                throw new Exception("The entity is has already");


            _appDbContext.Storytellers.Add(storyteller);
            Save();
            return storyteller.Id;
        }

        public void DeleteStorytellerById(string id)
        {
            if (id != null)
            {
                Storyteller storyteller = FindStorytellerById(id);
                if (storyteller != null)
                {
                    _appDbContext.Storytellers.Remove(storyteller);
                    Save();
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

        public Storyteller UpdateStoryteller(Storyteller storyteller)
        {
            if (storyteller != null)
            {
                var entry = FindStorytellerById(storyteller.Id);
                if (entry != null)
                {
                    _appDbContext.Entry(entry).CurrentValues.SetValues(storyteller);
                    Save();
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

        private void Save()
        {
            _appDbContext.SaveChanges();
            _appDbContext.Dispose();
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