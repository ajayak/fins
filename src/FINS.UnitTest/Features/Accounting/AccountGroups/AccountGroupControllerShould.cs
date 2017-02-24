﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using FINS.Features.Accounting.AccountGroups;
using FINS.Models.Accounting;
using FINS.Models.App;
using FINS.Security;
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

        public AccountGroupControllerShould()
        {
            _sut = new AccountGroupController(Mediator);
        }

        [Fact]
        public async void ReturnAllAccountGroupForUser()
        {
            var user = new ClaimsPrincipal(new GenericPrincipal(new GenericIdentity("user"), null));
            user.AddIdentity(new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Organization, OrganizationId.ToString()),
                new Claim(ClaimTypes.UserType, "OrgAdmin")
            }));

            _sut.ControllerContext.HttpContext = new DefaultHttpContext();
            _sut.HttpContext.User = user;

            var result = await _sut.GetAllAccountTypes();
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

            var result = await _sut.GetAllAccountTypes(OrganizationId);
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.As<List<AccountGroupDto>>().Count.Should().Be(3);
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
        }
    }
}