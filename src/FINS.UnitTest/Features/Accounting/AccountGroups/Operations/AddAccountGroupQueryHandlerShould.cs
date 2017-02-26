using System;
using FINS.Features.Accounting.AccountGroups.Operations;
using FINS.Models.Accounting;
using FluentAssertions;
using Xunit;

namespace FINS.UnitTest.Features.Accounting.AccountGroups.Operations
{
    [Collection("TestData")]
    public class AddAccountGroupQueryHandlerShould : InMemoryContextTest
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public async void ShouldAddAccountGroup(int parentId)
        {
            var query = new AddAccountGroupQuery()
            {
                DisplayName = "test_add",
                Name = "test_add",
                ParentId = parentId,
                IsPrimary = false
            };
            var sut = new AddAccountGroupQueryHandler(Context);
            var result = await sut.Handle(query);
            result.ParentId.Should().Be(parentId);
            result.IsPrimary.Should().BeTrue();
        }

        [Fact]
        public async void ShouldMakeChangeParentAccounttoNotPrimary()
        {
            var guid = new Guid().ToString();
            var accountGroup = new AccountGroup()
            {
                Name = guid,
                DisplayName = guid,
                IsPrimary = true,
                ParentId = 0,
                OrganizationId = 1
            };
            await Context.AccountGroups.AddAsync(accountGroup);
            await Context.SaveChangesAsync();

            var query = new AddAccountGroupQuery()
            {
                DisplayName = "test_add",
                Name = "test_add",
                ParentId = accountGroup.Id
            };
            var sut = new AddAccountGroupQueryHandler(Context);
            await sut.Handle(query);
            var accountGroupUpdated = await Context.AccountGroups.FindAsync(accountGroup.Id);
            accountGroupUpdated.IsPrimary.Should().BeFalse();
        }

        [Fact]
        public async void ReturnErrorWhenAddAccountGroupAtChildWithParentNotExist()
        {
            var query = new AddAccountGroupQuery
            {
                DisplayName = "Test1232",
                Name = "Test12322",
                IsPrimary = true,
                ParentId = 213213123
            };
            var sut = new AddAccountGroupQueryHandler(Context);
            var ex = await Assert.ThrowsAsync<Exception>(async () => await sut.Handle(query));
            ex.Message.Should().Be("Parent organization does not exist");
        }
    }
}
