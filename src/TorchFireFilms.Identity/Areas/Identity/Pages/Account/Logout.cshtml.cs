using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TorchFireFilms.Identity.Data;

namespace TorchFireFilms.Identity.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly IIdentityServerInteractionService _interaction;

        public LogoutModel(
            SignInManager<ApplicationUser> signInManager,
            ILogger<LogoutModel> logger,
            IIdentityServerInteractionService interaction)
        {
            _signInManager = signInManager;
            _logger = logger;
            _interaction = interaction;
        }

        public async Task<IActionResult> OnGet(string logoutId = null)
        {
            return await this.OnPost(null, logoutId);
        }

        public async Task<IActionResult> OnPost(string returnUrl = null, string logoutId = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            // var logoutId = this.Request.Query["logoutId"].ToString();

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else if (logoutId != null)
            {
                var logoutContext = await this._interaction.GetLogoutContextAsync(logoutId);
                returnUrl = logoutContext.PostLogoutRedirectUri;

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }
                else
                {
                    return Page();
                }
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
