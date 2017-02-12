using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FINS.Context;
using FINS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FINS.DataAccess
{
    public class SampleDataGenerator
    {
        private readonly FinsDbContext _context;
        private readonly SampleDataSettings _settings;
        private readonly GeneralSettings _generalSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SampleDataGenerator(FinsDbContext context, IOptions<SampleDataSettings> options,
            IOptions<GeneralSettings> generalSettings, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _settings = options.Value;
            _generalSettings = generalSettings.Value;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async void InsertTestData()
        {
            await InsertOrganizations();
            await CreateUsers();
        }

        private async Task InsertOrganizations()
        {
            var organizationExists = await _context.Organizations.AnyAsync();
            if (!organizationExists)
            {
                var fsTenant = new Organization()
                {
                    Name = "fs",
                    Summary = "Test organization",
                    DescriptionHtml = "<h1>Description</h1>",
                    LogoUrl = "https://www.gravatar.com/avatar/c1dac1f4ff42afb6cbf5761039e79e3d",
                    PrivacyPolicy = "Do not copy",
                    PrivacyPolicyUrl = "https://www.google.co.in",
                    WebUrl = "http://www.github.com/"
                };
                await _context.Organizations.AddAsync(fsTenant);
            }

            var roleExists = await _context.Roles.AnyAsync();
            if (!roleExists)
            {
                var gateKeeper = new IdentityRole("GateKeeper");
                var testClaim = new Claim(Security.ClaimTypes.AccessLevel, "TEstClaim");
                var testClaim2 = new Claim(Security.ClaimTypes.AccessLevel, "TEstClaim2");
                await _roleManager.CreateAsync(gateKeeper);
                await _roleManager.AddClaimAsync(gateKeeper, testClaim);
                await _roleManager.AddClaimAsync(gateKeeper, testClaim2);
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Creates a administrative user who can manage the inventory.
        /// </summary>
        public async Task CreateUsers()
        {
            var adminUser = await _userManager.FindByNameAsync(_settings.DefaultAdminUsername);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    FirstName = "FirstName4",
                    LastName = "LastName4",
                    UserName = _settings.DefaultAdminUsername,
                    Email = _settings.DefaultAdminUsername,
                    EmailConfirmed = true,
                    PhoneNumber = "444-444-4444"
                };
                await _userManager.CreateAsync(adminUser, _settings.DefaultAdminPassword);
                await _userManager.AddClaimsAsync(adminUser, new List<Claim>()
                {
                    new Claim(Security.ClaimTypes.UserType, "SiteAdmin"),
                    new Claim(Security.ClaimTypes.UserType, "OrgAdmin")
                });

                var organization = _context.Organizations.FirstOrDefault(c => c.Name == "fs");
                var orgAdmin = new ApplicationUser
                {
                    FirstName = "FirstName5",
                    LastName = "LastName5",
                    UserName = _settings.DefaultOrganizationUsername,
                    Email = _settings.DefaultOrganizationUsername,
                    EmailConfirmed = true,
                    PhoneNumber = "555-555-5555",
                    Organization = organization
                };
                // For the sake of being able to exercise Organization-specific stuff, we need to associate a organization.
                await _userManager.CreateAsync(orgAdmin, _settings.DefaultAdminPassword);
                await _userManager.AddClaimAsync(orgAdmin, new Claim(Security.ClaimTypes.UserType, "OrgAdmin"));

                var orgUser = new ApplicationUser
                {
                    FirstName = "FirstName5",
                    LastName = "LastName5",
                    UserName = _settings.DefaultUsername,
                    Email = _settings.DefaultUsername,
                    EmailConfirmed = true,
                    PhoneNumber = "666-666-6666",
                    Organization = organization
                };
                await _userManager.CreateAsync(orgUser, _settings.DefaultAdminPassword);
                await _userManager.AddClaimAsync(orgUser, new Claim(Security.ClaimTypes.UserType, "BasicUser"));

                await _userManager.AddToRoleAsync(orgUser, "GateKeeper");
            }
        }
    }
}
