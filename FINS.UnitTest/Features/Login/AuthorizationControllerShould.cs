using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using FINS.Features.Login;
using FINS.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using SignInResult = Microsoft.AspNetCore.Mvc.SignInResult;

namespace FINS.UnitTest.Features.Login
{
    [Collection("OrgUserRole")]
    public class AuthorizationControllerShould : InMemoryContextTest
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private AuthorizationController _sut;

        public AuthorizationControllerShould()
        {
            _signInManager = ServiceProvider.GetService<SignInManager<ApplicationUser>>();
            _sut = new AuthorizationController(_signInManager, UserManager, Mediator);
        }

        [Theory]
        [InlineData("")]
        [InlineData("fs")]
        [InlineData("fso")]
        public async Task ReturnsTokenForSiteAdminWithAnyOrg(string orgName)
        {
            var request = new OpenIdConnectRequest()
            {
                Username = "Administrator@example.com",
                Password = "YouShouldChangeThisPassword1!",
                GrantType = "password",
                Scope = "openid email profiles roles",
            };
            request.AddParameter("organization", orgName);
            var result = await _sut.Exchange(request);

            result.Should().BeOfType<SignInResult>()
                .Which.Principal.Identity.Name.Should().Be("Administrator@example.com");

            result.As<SignInResult>().Principal.Identity.IsAuthenticated.Should().BeTrue();
        }

        [Fact]
        public async Task ReturnsTokenForOrgAdminWithValidOrg()
        {
            var request = new OpenIdConnectRequest()
            {
                Username = "organization@example.com",
                Password = "YouShouldChangeThisPassword1!",
                GrantType = "password",
                Scope = "openid email profiles roles",
            };
            request.AddParameter("organization", "fs");
            var result = await _sut.Exchange(request);

            result.Should().BeOfType<SignInResult>()
                .Which.Principal.Identity.Name.Should().Be("organization@example.com");

            result.As<SignInResult>().Principal.Identity.IsAuthenticated.Should().BeTrue();
        }

        [Fact]
        public async Task ReturnsErrorForUserWithInOrgNotExists()
        {
            var nonExistingOrg = "notfso";
            var request = new OpenIdConnectRequest()
            {
                Username = "organization@example.com",
                Password = "YouShouldChangeThisPassword1!",
                GrantType = "password",
                Scope = "openid email profiles roles",
            };
            request.AddParameter("organization", nonExistingOrg);
            var result = await _sut.Exchange(request);

            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be(400);

            var error = result.As<BadRequestObjectResult>()
                .Value.As<OpenIdConnectResponse>();

            error.Error.Should().Be(OpenIdConnectConstants.Errors.AccessDenied);
            error.ErrorDescription.Should().Be($"{nonExistingOrg} Organization does not exists");
        }

        [Fact]
        public async Task ReturnsErrorForUserWithInvalidOrg()
        {
            var invalidOrgName = "anotherfs";
            var userName = "organization@example.com";
            var request = new OpenIdConnectRequest()
            {
                Username = userName,
                Password = "YouShouldChangeThisPassword1!",
                GrantType = "password",
                Scope = "openid email profiles roles",
            };
            request.AddParameter("organization", invalidOrgName);
            var result = await _sut.Exchange(request);

            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be(400);

            var error = result.As<BadRequestObjectResult>()
                .Value.As<OpenIdConnectResponse>();

            error.Error.Should().Be(OpenIdConnectConstants.Errors.AccessDenied);
            error.ErrorDescription.Should().Be($"{userName} is not a member of {invalidOrgName}.");
        }

        [Fact]
        public async void ReturnsErrorWhenInvalidGrantType()
        {
            var result = await _sut.Exchange(new OpenIdConnectRequest()
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

        [Fact]
        public async void ReturnsErrorWhenPasswordIsWrong()
        {
            var request = new OpenIdConnectRequest()
            {
                Username = "user@example.com",
                Password = "wrong!password",
                GrantType = "password",
                Scope = "openid email profiles roles",
            };
            request.AddParameter("organization", "fs");
            var result = await _sut.Exchange(request);

            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be(400);

            var error = result.As<BadRequestObjectResult>()
                .Value.As<OpenIdConnectResponse>();

            error.Error.Should().Be(OpenIdConnectConstants.Errors.InvalidGrant);
            error.ErrorDescription.Should().Be("The username/password couple is invalid.");
        }
    }
}
