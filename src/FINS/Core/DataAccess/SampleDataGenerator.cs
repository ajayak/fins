using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FINS.Context;
using FINS.Core.Configuration;
using FINS.Models;
using FINS.Models.Accounting;
using FINS.Models.App;
using FINS.Models.Common;
using FINS.Models.Inventory;
using FINS.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FINS.Core.DataAccess
{
    public class SampleDataGenerator
    {
        private readonly FinsDbContext _context;
        private readonly SampleDataSettings _settings;
        private readonly GeneralSettings _generalSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public int OrganizationId { get; set; }

        public SampleDataGenerator(FinsDbContext context, IOptions<SampleDataSettings> options,
            IOptions<GeneralSettings> generalSettings, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _settings = options.Value;
            _generalSettings = generalSettings.Value;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InsertDemoData()
        {
            await InsertMasterData();
            await InsertOrgUserRoleData();
            await InsertAccountData();
            await InsertInventoriesData();
        }

        public async Task InsertMasterData()
        {
            if (_context.States.Any()) return;

            var states = new List<State>
            {
                new State{Code = "AP",Name = "Andhra Pradesh"},
                new State{Code = "AR",Name = "Arunachal Pradesh"},
                new State{Code = "AS",Name = "Assam"},
                new State{Code = "BR",Name = "Bihar"},
                new State{Code = "CG",Name = "Chhattisgarh"},
                new State{Code = "CH",Name = "Chandigarh"},
                new State{Code = "DN",Name = "Dadra and Nagar Haveli"},
                new State{Code = "DD",Name = "Daman and Diu"},
                new State{Code = "DL",Name = "Delhi"},
                new State{Code = "GA",Name = "Goa"},
                new State{Code = "GJ",Name = "Gujarat"},
                new State{Code = "HR",Name = "Haryana"},
                new State{Code = "HP",Name = "Himachal Pradesh"},
                new State{Code = "JK",Name = "Jammu and Kashmir"},
                new State{Code = "JH",Name = "Jharkhand"},
                new State{Code = "KA",Name = "Karnataka"},
                new State{Code = "KL",Name = "Kerala"},
                new State{Code = "MP",Name = "Madhya Pradesh"},
                new State{Code = "MH",Name = "Maharashtra"},
                new State{Code = "MN",Name = "Manipur"},
                new State{Code = "ML",Name = "Meghalaya"},
                new State{Code = "MZ",Name = "Mizoram"},
                new State{Code = "NL",Name = "Nagaland"},
                new State{Code = "OR",Name = "Orissa"},
                new State{Code = "PB",Name = "Punjab"},
                new State{Code = "PY",Name = "Pondicherry"},
                new State{Code = "RJ",Name = "Rajasthan"},
                new State{Code = "SK",Name = "Sikkim"},
                new State{Code = "TN",Name = "Tamil Nadu"},
                new State{Code = "TR",Name = "Tripura"},
                new State{Code = "UP",Name = "Uttar Pradesh"},
                new State{Code = "UK",Name = "Uttarakhand"},
                new State{Code = "WB",Name = "West Benga"}
            };
            await _context.States.AddRangeAsync(states);
            await _context.SaveChangesAsync();
        }

        public async Task InsertOrgUserRoleData()
        {
            if (_context.Roles.Any() ||
                _context.Users.Any())
            {
                OrganizationId = _context.Organizations
                .Where(c => c.Name == "fs")
                .Select(c => c.Id)
                .FirstOrDefault();
                return;
            }

            #region Organization

            var organization = new Organization()
            {
                Name = "fs",
                Code = "fs",
                Summary = "Test organization",
                DescriptionHtml = "<h1>Description</h1>",
                LogoUrl = "https://www.gravatar.com/avatar/c1dac1f4ff42afb6cbf5761039e79e3d",
                PrivacyPolicy = "Do not copy",
                PrivacyPolicyUrl = "https://www.google.co.in",
                WebUrl = "http://www.github.com/"
            };
            var anotherOrganization = new Organization()
            {
                Name = "anotherfs",
                Code = "anotherfs",
                Summary = "Test organization 2",
                DescriptionHtml = "<h1>Description</h1>",
                LogoUrl = "https://www.gravatar.com/avatar/c1dac1f4ff42afb6cbf5761039e79e3d",
                PrivacyPolicy = "Do not copy",
                PrivacyPolicyUrl = "https://www.google.co.in",
                WebUrl = "http://www.github.com/"
            };
            await _context.Organizations.AddAsync(organization);
            await _context.Organizations.AddAsync(anotherOrganization);
            await _context.SaveChangesAsync();
            OrganizationId = organization.Id;

            #endregion

            #region Roles

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

            #endregion

            #region Users

            var adminUser = new ApplicationUser
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
                    new Claim(Security.ClaimTypes.UserType, "OrgAdmin"),
                    new Claim(Security.ClaimTypes.Accounting, Accounting.AccountGroupManager.ToString())
                });

            var orgAdmin = new ApplicationUser
            {
                FirstName = "FirstName5",
                LastName = "LastName5",
                UserName = _settings.DefaultOrganizationUsername,
                Email = _settings.DefaultOrganizationUsername,
                EmailConfirmed = true,
                PhoneNumber = "555-555-5555"
            };
            // For the sake of being able to exercise Organization-specific stuff, we need to associate a organization.
            await _userManager.CreateAsync(orgAdmin, _settings.DefaultAdminPassword);
            await _userManager.AddClaimsAsync(orgAdmin, new List<Claim>()
                {
                    new Claim(Security.ClaimTypes.UserType, "OrgAdmin"),
                    new Claim(Security.ClaimTypes.Organization, organization.Id.ToString()),
                    new Claim(Security.ClaimTypes.Accounting, Accounting.AccountGroupManager.ToString())
                });

            var orgUser = new ApplicationUser
            {
                FirstName = "FirstName5",
                LastName = "LastName5",
                UserName = _settings.DefaultUsername,
                Email = _settings.DefaultUsername,
                EmailConfirmed = true,
                PhoneNumber = "666-666-6666"
            };
            await _userManager.CreateAsync(orgUser, _settings.DefaultAdminPassword);
            await _userManager.AddClaimsAsync(orgUser, new List<Claim>
                {
                    new Claim(Security.ClaimTypes.UserType, "BasicUser"),
                    new Claim(Security.ClaimTypes.Organization, organization.Id.ToString())
                });

            await _userManager.AddToRoleAsync(orgUser, "GateKeeper");

            #endregion
        }

        public async Task InsertAccountData()
        {
            if (_context.AccountGroups.Any())
            {
                return;
            }

            var accountGroups = new List<AccountGroup>
            {
                new AccountGroup(){Name = "Loan", DisplayName = "Loan", IsPrimary = true, ParentId = 0, OrganizationId = OrganizationId},
                new AccountGroup(){Name = "Sales", DisplayName = "Sales", IsPrimary = true, ParentId = 0, OrganizationId = OrganizationId},
                new AccountGroup(){Name = "Purchase", DisplayName = "Purchase", IsPrimary = true, ParentId = 0, OrganizationId = OrganizationId}
            };
            await _context.AccountGroups.AddRangeAsync(accountGroups);

            var accounts = new List<Account>
            {
                new Account()
                {
                    AccountGroup = accountGroups[0],
                    DisplayName = "Account1",
                    Name = "Account1",
                    Code = "A1",
                    OpeningBalance = 10,
                    OpeningBalanceType = TransactionType.Credit,
                    Address = "Gobindgarh",
                    ItPanNumber = "123456789",
                    CstNumber = "123456789",
                    ServiceTaxNumber = "123456789",
                    TinNumber = "123456789",
                    Ward = "123456789",
                    StateId = 1,
                    ContactPersons = new List<Person>()
                    {
                        new Person()
                        {
                            FirstName = "ABC",
                            LastName = "DEF",
                            EmailId = "a@a.com",
                            Mobile = 987987987
                        }
                    }
                },
                new Account()
                {
                    AccountGroup = accountGroups[1],
                    DisplayName = "Account2",
                    Name = "Account2",
                    Code = "A2",
                    Address = "Sirhind",
                    ItPanNumber = "123456789",
                    CstNumber = "123456789",
                    ServiceTaxNumber = "123456789",
                    TinNumber = "123456789",
                    Ward = "123456789",
                    StateId = 1,
                    ContactPersons = new List<Person>()
                    {
                        new Person()
                        {
                            FirstName = "XYZ",
                            LastName = "PQL",
                            EmailId = "aasds@a.com",
                            Mobile = 987987987
                        }
                    }
                }
            };
            await _context.Accounts.AddRangeAsync(accounts);

            await _context.SaveChangesAsync();
        }

        public async Task InsertInventoriesData()
        {
            if (_context.ItemGroups.Any())
            {
                return;
            }

            var items = new List<ItemGroup>
            {
                new ItemGroup(){Name = "Hardware Tools", DisplayName = "Hardware Tools", IsPrimary = true, ParentId = 0, OrganizationId = OrganizationId},
                new ItemGroup(){Name = "Stationary", DisplayName = "Stationary", IsPrimary = true, ParentId = 0, OrganizationId = OrganizationId}
            };
            await _context.ItemGroups.AddRangeAsync(items);
        }
    }
}
