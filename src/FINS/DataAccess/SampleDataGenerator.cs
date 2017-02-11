using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FINS.Context;
using FINS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace FINS.DataAccess
{
    public class SampleDataGenerator
    {
        private readonly FinsDbContext _context;
        private readonly SampleDataSettings _settings;
        private readonly GeneralSettings _generalSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TimeZoneInfo _timeZone = TimeZoneInfo.Local;

        public SampleDataGenerator(FinsDbContext context, IOptions<SampleDataSettings> options,
            IOptions<GeneralSettings> generalSettings, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _settings = options.Value;
            _generalSettings = generalSettings.Value;
            _userManager = userManager;
        }

        public void InsertTestData()
        {
        }

        /// <summary>
        /// Creates a administrative user who can manage the inventory.
        /// </summary>
        public async Task CreateAdminUser()
        {
            var user = await _userManager.FindByNameAsync(_settings.DefaultAdminUsername);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    FirstName = "FirstName4",
                    LastName = "LastName4",
                    UserName = _settings.DefaultAdminUsername,
                    Email = _settings.DefaultAdminUsername,
                    EmailConfirmed = true,
                    PhoneNumber = "444-444-4444"
                };
                _userManager.CreateAsync(user, _settings.DefaultAdminPassword).GetAwaiter().GetResult();
                _userManager.AddClaimAsync(user, new Claim(Security.ClaimTypes.UserType, "SiteAdmin")).GetAwaiter().GetResult();

                var user2 = new ApplicationUser
                {
                    FirstName = "FirstName5",
                    LastName = "LastName5",
                    UserName = _settings.DefaultOrganizationUsername,
                    Email = _settings.DefaultOrganizationUsername,
                    EmailConfirmed = true,
                    PhoneNumber = "555-555-5555"
                };
                // For the sake of being able to exercise Organization-specific stuff, we need to associate a organization.
                await _userManager.CreateAsync(user2, _settings.DefaultAdminPassword);
                await _userManager.AddClaimAsync(user2, new Claim(Security.ClaimTypes.UserType, "OrgAdmin"));

                var user3 = new ApplicationUser
                {
                    FirstName = "FirstName5",
                    LastName = "LastName5",
                    UserName = _settings.DefaultUsername,
                    Email = _settings.DefaultUsername,
                    EmailConfirmed = true,
                    PhoneNumber = "666-666-6666"
                };
                await _userManager.CreateAsync(user3, _settings.DefaultAdminPassword);
            }
        }
    }
}
