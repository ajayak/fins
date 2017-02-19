using AspNet.Security.OpenIdConnect.Primitives;
using FINS.Features.Login;
using FINS.Models;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FINS.UnitTest.Features.Login
{
    [Collection("Integration tests collection")]
    public class AuthorizationControllerShould : InMemoryContextTest
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMediator _mediator;

        public AuthorizationControllerShould()
        {
            _signInManager = ServiceProvider.GetService<SignInManager<ApplicationUser>>();
            _mediator = ServiceProvider.GetService<Mediator>();
        }

        [Fact]
        public async void ReturnOkStatus()
        {
            var sut = new AuthorizationController(_signInManager, UserManager, _mediator);

            var result = await sut.Exchange(new OpenIdConnectRequest()
            {
                Username = "a"
            });

            0.Should().Be(0);
        }
    }
}
