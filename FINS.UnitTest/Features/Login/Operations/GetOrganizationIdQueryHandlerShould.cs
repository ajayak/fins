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
    public class GetOrganizationIdQueryHandlerShould : InMemoryContextTest, IClassFixture<OrganizationDataFixture>
    {
        [Theory]
        [InlineData("testOrg")]
        [InlineData("TESTORG")]
        public async Task ReturnOrganizationIdWhenOrgExists(string name)
        {
            var sut = new GetOrganizationIdQueryHandler(Context);
            var result = await sut.Handle(new GetOrganizationIdQuery {OrganizationName = name });
            result.Should().Be(1);
        }

        [Theory]
        [InlineData("fsa")]
        [InlineData("")]
        public async Task ReturnsZeroWhenOrgNameNotExists(string name)
        {
            var sut = new GetOrganizationIdQueryHandler(Context);
            var result = await sut.Handle(new GetOrganizationIdQuery { OrganizationName = name });
            result.Should().Be(0);
        }
    }

    public class OrganizationDataFixture : InMemoryContextTest
    {
        public OrganizationDataFixture()
        {
            var organization = new Organization()
            {
                Name = "testOrg",
                Code = "to",
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
    }
}
