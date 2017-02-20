using System.Linq;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using FINS.DataAccess;
using FINS.Features.Login;
using FINS.Models;
using FINS.Security;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using SignInResult = Microsoft.AspNetCore.Mvc.SignInResult;

namespace FINS.UnitTest.Features.Login
{
    public class DataFixture:TestBase
    {
        public DataFixture()
        {
            var dataGenerator = ServiceProvider.GetService<SampleDataGenerator>();
            dataGenerator.InsertOrgUserRoleTestData().Wait();
        }
    }

    public class AuthorizationControllerShould : InMemoryContextTest, IClassFixture<DataFixture>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthorizationControllerShould()
        {
            _signInManager = ServiceProvider.GetService<SignInManager<ApplicationUser>>();
        }

        [Fact]
        public async Task ReturnsTokenForOrgAdminWithValidOrg()
        {
            await SampleDataGenerator.InsertOrgUserRoleTestData();
            var sut = new AuthorizationController(_signInManager, UserManager, Mediator);
            var request = new OpenIdConnectRequest()
            {
                Username = "organization@example.com",
                Password = "YouShouldChangeThisPassword1!",
                GrantType = "password",
                Scope = "openid email profiles roles",
            };
            request.AddParameter("organization", "fs");
            var result = await sut.Exchange(request);

            result.Should().BeOfType<SignInResult>()
                .Which.Principal.Identity.Name.Should().Be("organization@example.com");

            result.As<SignInResult>().Principal.Identity.IsAuthenticated.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData("fs")]
        [InlineData("fso")]
        public async Task ReturnsTokenForSiteAdminWithAnyOrg(string orgName)
        {
            await SampleDataGenerator.InsertOrgUserRoleTestData();
            var sut = new AuthorizationController(_signInManager, UserManager, Mediator);
            var request = new OpenIdConnectRequest()
            {
                Username = "Administrator@example.com",
                Password = "YouShouldChangeThisPassword1!",
                GrantType = "password",
                Scope = "openid email profiles roles",
            };
            request.AddParameter("organization", orgName);
            var result = await sut.Exchange(request);

            result.Should().BeOfType<SignInResult>()
                .Which.Principal.Identity.Name.Should().Be("Administrator@example.com");

            result.As<SignInResult>().Principal.Identity.IsAuthenticated.Should().BeTrue();
        }

        [Fact]
        public async void ReturnsErrorWhenInvalidGrantType()
        {
            await SampleDataGenerator.InsertOrgUserRoleTestData();
            var sut = new AuthorizationController(_signInManager, UserManager, Mediator);
            var result = await sut.Exchange(new OpenIdConnectRequest()
            {
                Username = "Administrator@example.com",
                Password = "YouShouldChangeThisPassword1!",
                GrantType = "not!password",
                Scope = "openid email profiles roles",
            });

            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be(400);

            result.As<BadRequestObjectResult>()
                .Value.As<OpenIdConnectResponse>()
                .Error.Should().Be(OpenIdConnectConstants.Errors.UnsupportedGrantType);
        }
    }
}
