using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using StoryHub.BL.Models;
using StoryHub.BL.Services.Abstract;

namespace StoryHub.WebUI.Areas.Identity.Pages.Account.Manager
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<Storyteller> _userManager;
        private readonly SignInManager<Storyteller> _signInManager;
        private readonly IStorytellerCRUD _storytellerCRUD;
        private readonly IHostEnvironment _environment;

        public IndexModel(
            UserManager<Storyteller> userManager,
            SignInManager<Storyteller> signInManager,
            IStorytellerCRUD storytellerCRUD,
            IHostEnvironment environment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _storytellerCRUD = storytellerCRUD;
            _environment = environment;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Image { get; set; }
            public string About { get; set; }
            public bool Gender { get; set; }
            [DefaultValue(0)]
            public int Age { get; set; }
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(Storyteller user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Gender = user.Gender,
                About = user.About,
                Age = user.Age,
                Image = user.Image,
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            #region Initialization new Storyteller

            string image = AddImage();

            if (string.IsNullOrWhiteSpace(image))
                image = user.Image;

            if (Input.About == null)
                Input.About = user.About;

            if (Input.Age == 0)
                Input.Age = user.Age;

            var storyteller = new Storyteller(user.Name,
                                              Input.Gender,
                                              image,
                                              Input.About,
                                              Input.Age)
            {
                Id = user.Id,
                Email = user.Email,
                LockoutEnd = user.LockoutEnd,
                NormalizedEmail = user.NormalizedEmail,
                NormalizedUserName = user.NormalizedUserName,
                PasswordHash = user.PasswordHash,
                PhoneNumber = Input.PhoneNumber,
                UserName = user.Name,
            };
            #endregion

            await _storytellerCRUD.UpdateStoryteller(storyteller);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        private string AddImage()
        {
            var newImageName = string.Empty;
            var imgExtensions = new List<string> { ".jpg", ".png", ".jpeg", ".gif" };

            if (HttpContext.Request.Form.Files != null)
            {
                var files = HttpContext.Request.Form.Files;

                foreach (IFormFile file in files)
                {
                    if (file.Length > 0)
                    {
                        var imageName = string.Empty;
                        imageName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var fileExtension = Path.GetExtension(imageName);

                        if (imgExtensions.Contains(fileExtension))
                        {
                            //Assigning Unique Filename (Guid)
                            var uniqueFileName = Convert.ToString(Guid.NewGuid());

                            newImageName = uniqueFileName + fileExtension;

                            // Path
                            imageName = Path.Combine(_environment.ContentRootPath, "wwwroot\\imagesStorytellers") + $@"\{newImageName}";

                            using (FileStream fs = System.IO.File.Create(imageName))
                            {
                                file.CopyTo(fs);
                                fs.Flush();
                            }
                        }
                    }
                }
            }

            return newImageName;
        }
    }
}
