using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StoryHub.BL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace StoryHub.WebUI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Storyteller> _signInManager;
        private readonly UserManager<Storyteller> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IHostEnvironment _environment;

        public RegisterModel(
            UserManager<Storyteller> userManager,
            SignInManager<Storyteller> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IHostEnvironment environment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _environment = environment;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Enter your name")]
            [StringLength(30, MinimumLength = 3, ErrorMessage = "The name cannot be more than 30 characters")]
            public string Name { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Display(Name = "Profile image (unrequired)")]
            public string Image { get; set; }

            public bool Gender { get; set; }

            [Required]
            [Range(1, 100, ErrorMessage = "Enter valid age (1 - 100)")]
            [DefaultValue(18)]
            public int Age { get; set; }

            [Display(Name = "Information about you")]
            [StringLength(50, ErrorMessage = "Information about you cannot be more than 30 characters")]
            public string About { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                string imageName = AddImage();
                var user = new Storyteller(Input.Name, Input.Gender, imageName, Input.About, Input.Age)
                {
                    UserName = Input.Name,
                    Email = Input.Email.ToLower(),
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code, returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private string AddImage()
        {
            const string defaultImage = "defaultImageStoryteller.jpg";
            string newImageName;
            var imgExtensions = new List<string> { ".jpg", ".png", ".jpeg", ".gif"};

            if (HttpContext.Request.Form.Files != null)
            {
                string imageName;

                var files = HttpContext.Request.Form.Files;

                foreach (IFormFile file in files)
                {
                    if (file.Length > 0)
                    {
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

                            return newImageName;
                        }
                    }
                }
            }

            return defaultImage;
        }
    }
}
