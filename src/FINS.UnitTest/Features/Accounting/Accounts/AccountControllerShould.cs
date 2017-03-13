using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using FINS.Core.Helpers;
using FINS.Features.Accounting.AccountGroups;
using FINS.Features.Accounting.Accounts;
using FINS.Features.Accounting.Accounts.DTO;
using FINS.Features.Accounting.Accounts.Operations;
using FINS.Models;
using FINS.Models.Accounting;
using FINS.Models.App;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using ClaimTypes = FINS.Security.ClaimTypes;

namespace FINS.UnitTest.Features.Accounting.Accounts
{
    [Collection("TestData")]
    public class AccountControllerShould : InMemoryContextTest
    {
        private readonly AccountController _sut;
        public int OrganizationId { get; set; }

        public AccountControllerShould()
        {
            _sut = new AccountController(Mediator);
        }

        [Fact]
        public async void ReturnPaginatedAccounts()
        {
            _sut.ControllerContext.HttpContext = new DefaultHttpContext();
            _sut.HttpContext.User = CreateOrgAdminUser();
            var result = await _sut.GetAllAccounts();
            result.Should().BeOfType<OkObjectResult>();
            var dto = result.As<OkObjectResult>().Value.As<PagedResult<AccountListDto>>();
            dto.PageNo.Should().Be(1);
            dto.PageSize.Should().Be(10);
            dto.Items.Count.Should().BeGreaterThan(1);
        }

        private ClaimsPrincipal CreateOrgAdminUser()
        {
            var user = new ClaimsPrincipal(new GenericPrincipal(new GenericIdentity("user"), null));
            user.AddIdentity(new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Organization, OrganizationId.ToString()),
                new Claim(ClaimTypes.UserType, "OrgAdmin")
            }));
            return user;
        }

        protected override void LoadTestData()
        {
            var guid = new Guid().ToString();
            var organization = new Organization() { Name = guid, Code = guid };
            Context.Organizations.Add(organization);
            Context.SaveChanges();
            OrganizationId = organization.Id;
            var accountGroups = new List<AccountGroup>
            {
                new AccountGroup(){Name = $"TG1{guid}", DisplayName = $"TG1{guid}", IsPrimary = true, ParentId = 0, OrganizationId = OrganizationId},
                new AccountGroup(){Name = $"TG2{guid}", DisplayName =$"TG2{guid}", IsPrimary = true, ParentId = 0, OrganizationId = OrganizationId},
            };
            Context.AccountGroups.AddRange(accountGroups);
            var child = new AccountGroup()
            {
                Name = $"TG3{guid}",
                DisplayName = $"TG3{guid}",
                IsPrimary = true,
                ParentId = accountGroups.Last().Id,
                OrganizationId = OrganizationId
            };
            Context.AccountGroups.Add(child);

            var accounts = new List<Account>
            {
                new Account()
                {
                    AccountGroup = accountGroups[0],
                    DisplayName = "Account1",
                    Name = "Account1",
                    Code = "A1",
                    OpeningBalance = 10,
                    Address = "Gobindgarh",
                    ItPanNumber = "123456789",
                    CstNumber = "123456789",
                    ServiceTaxNumber = "123456789",
                    TinNumber = "123456789",
                    Ward = "123456789",
                    StateId = 1,
                    OpeningBalanceType = TransactionType.Credit,
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
                    AccountGroup = child,
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
            Context.Accounts.AddRange(accounts);
            Context.SaveChanges();
        }
    }
}
