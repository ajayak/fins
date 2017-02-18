using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FINS.Features.Login.Operations;
using FINS.Models;
using FluentAssertions;
using Xunit;

namespace FINS.UnitTest.Features.Login.Operations
{
    public class GetOrganizationIdQueryHandlerShould : InMemoryContextTest
    {
        protected override void LoadTestData()
        {
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
            Context.Organizations.Add(organization);
            Context.SaveChanges();
        }

        [Fact]
        public async void ReturnOrganizationIdWhenOrgExists()
        {
            var sut = new GetOrganizationIdQueryHandler(Context);
            var result = await sut.Handle(new GetOrganizationIdQuery {OrganizationName = "fs"});
            result.Should().Be(1);
        }

        [Fact]
        public async void ReturnOrganizationIdWhenOrgNameIsUpper()
        {
            var sut = new GetOrganizationIdQueryHandler(Context);
            var result = await sut.Handle(new GetOrganizationIdQuery { OrganizationName = "FS" });
            result.Should().Be(1);
        }

        [Fact]
        public async void ReturnsZeroWhenOrgNameNotExists()
        {
            var sut = new GetOrganizationIdQueryHandler(Context);
            var result = await sut.Handle(new GetOrganizationIdQuery { OrganizationName = "fsa" });
            result.Should().Be(0);
        }
    }
}
