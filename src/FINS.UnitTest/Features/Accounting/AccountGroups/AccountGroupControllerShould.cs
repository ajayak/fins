using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using FINS.Features.Accounting.AccountGroups;
using FINS.Models.Accounting;
using FINS.Models.App;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using ClaimTypes = FINS.Security.ClaimTypes;

namespace FINS.UnitTest.Features.Accounting.AccountGroups
{
    [Collection("TestData")]
    public class AccountGroupControllerShould : InMemoryContextTest
    {
        private readonly AccountGroupController _sut;
        public int OrganizationId { get; set; }
        public int AccountGroupId { get; set; }

        public AccountGroupControllerShould()
        {
            _sut = new AccountGroupController(Mediator);
        }

        [Fact]
        public async void ReturnAllAccountGroupForUser()
        {
            _sut.ControllerContext.HttpContext = new DefaultHttpContext();
            _sut.HttpContext.User = CreateOrgAdminUser();

            var result = await _sut.GetAllAccountGroups();
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.As<List<AccountGroupDto>>().Count.Should().Be(3);
        }

        [Fact]
        public async void ReturnAccountGroupForAdminUserWithNoOrganizationClaim()
        {
            var user = new ClaimsPrincipal(new GenericPrincipal(new GenericIdentity("user"), null));
            user.AddIdentity(new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.UserType, "SiteAdmin")
            }));

            _sut.ControllerContext.HttpContext = new DefaultHttpContext();
            _sut.HttpContext.User = user;

            var result = await _sut.GetAllAccountGroups(OrganizationId);
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.As<List<AccountGroupDto>>().Count.Should().Be(3);
        }

        [Fact]
        public async void AddAccountGroupAtRoot()
        {
            _sut.ControllerContext.HttpContext = new DefaultHttpContext();
            _sut.HttpContext.User = CreateOrgAdminUser();
            var accountGroupDto = new AccountGroupDto
            {
                DisplayName = "Test",
                Name = "Test",
                IsPrimary = true,
                ParentId = 0
            };
            var result = await _sut.AddAccountGroup(accountGroupDto);
            result.Should().BeOfType<OkObjectResult>();
            var dto = result.As<OkObjectResult>().Value.As<AccountGroupDto>();
            dto.Id.Should().NotBe(0);
            dto.DisplayName.Should().Be(accountGroupDto.DisplayName);
        }

        [Fact]
        public async void AddAccountGroupAtChild()
        {
            _sut.ControllerContext.HttpContext = new DefaultHttpContext();
            _sut.HttpContext.User = CreateOrgAdminUser();
            var accountGroupDto = new AccountGroupDto
            {
                DisplayName = "Test2",
                Name = "Test2",
                IsPrimary = true,
                ParentId = AccountGroupId
            };
            var result = await _sut.AddAccountGroup(accountGroupDto);
            result.Should().BeOfType<OkObjectResult>();
            var dto = result.As<OkObjectResult>().Value.As<AccountGroupDto>();
            dto.Id.Should().NotBe(0);
            dto.DisplayName.Should().Be(accountGroupDto.DisplayName);
            dto.ParentId.Should().Be(AccountGroupId);
        }

        [Fact]
        public async void ReturnErrorWhenAddAccountGroupAtChildWithParentNotExist()
        {
            _sut.ControllerContext.HttpContext = new DefaultHttpContext();
            _sut.HttpContext.User = CreateOrgAdminUser();
            var accountGroupDto = new AccountGroupDto
            {
                DisplayName = "Test2",
                Name = "Test2",
                IsPrimary = true,
                ParentId = 213213123
            };
            var result = await _sut.AddAccountGroup(accountGroupDto);
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>()
                .Value.As<string>()
                .Should().Be("Parent organization does not exist");
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
            Context.SaveChanges();
            AccountGroupId = child.Id;
        }
    }
}
