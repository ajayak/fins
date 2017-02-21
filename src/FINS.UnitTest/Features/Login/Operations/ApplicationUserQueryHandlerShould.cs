using FINS.Features.Login.Operations;
using FluentAssertions;
using Xunit;

namespace FINS.UnitTest.Features.Login.Operations
{
    [Collection("OrgUserRole")]
    public class ApplicationUserQueryHandlerShould : InMemoryContextTest
    {
        [Theory]
        [InlineData("Administrator@example.com")]
        [InlineData("User@EXAMPLE.com")]
        public async void ShouldReturnUserWhenUserIsFound(string username)
        {
            var query = new ApplicationUserQuery(){UserName = username};
            var sut = new ApplicationUserQueryHandler(Context);
            var result = await sut.Handle(query);

            result.Should().NotBeNull();
            result.UserName.ToLowerInvariant().Should().Be(username.ToLowerInvariant());
        }

        [Theory]
        [InlineData("unknown@somebody.com")]
        [InlineData("")]
        public async void ShouldReturnNullWhenUserIsNotFound(string username)
        {
            var query = new ApplicationUserQuery() { UserName = username };
            var sut = new ApplicationUserQueryHandler(Context);
            var result = await sut.Handle(query);

            result.Should().BeNull();
        }
    }
}
