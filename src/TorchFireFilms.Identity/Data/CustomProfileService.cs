﻿using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TorchFireFilms.Identity.Data
{
    public class CustomProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            if (context.Caller == IdentityServerConstants.ProfileDataCallers.UserInfoEndpoint)
            {
                context.IssuedClaims = new List<Claim>()
                {
                    new Claim("name", user.UserName ?? ""),
                    new Claim("preferred_username", user.UserName ?? ""),
                    new Claim("email", user.Email ?? ""),
                    new Claim("given_name", user.FirstName ?? ""),
                    new Claim("family_name", user.LastName ?? ""),
                };
            }

        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.FromResult(0);
        }
    }
}
