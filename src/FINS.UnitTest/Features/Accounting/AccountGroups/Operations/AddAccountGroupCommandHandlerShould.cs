using System;
using System.Collections.Generic;
using System.Linq;
using FINS.Features.Accounting.AccountGroups.Operations;
using FINS.Models.Accounting;
using FINS.Models.App;
using FluentAssertions;
using Xunit;

namespace FINS.UnitTest.Features.Accounting.AccountGroups.Operations
{
    [Collection("TestData")]
    public class AddAccountGroupCommandHandlerShould : InMemoryContextTest
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public async void ShouldAddAccountGroup(int parentId)
        {
            var query = new AddAccountGroupCommand()
            {
                DisplayName = "test_add",
                Name = "test_add",
                ParentId = parentId,
                IsPrimary = false
            };
            var sut = new AddAccountGroupCommandHandler(Context);
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

            var query = new AddAccountGroupCommand()
            {
                DisplayName = "test_add",
                Name = "test_add",
                ParentId = accountGroup.Id
            };
            var sut = new AddAccountGroupCommandHandler(Context);
            await sut.Handle(query);
            var accountGroupUpdated = await Context.AccountGroups.FindAsync(accountGroup.Id);
            accountGroupUpdated.IsPrimary.Should().BeFalse();
        }

        [Fact]
        public async void ReturnErrorWhenAddAccountGroupAtChildWithParentNotExist()
        {
            var query = new AddAccountGroupCommand
            {
                DisplayName = "Test1232",
                Name = "Test12322",
                IsPrimary = true,
                ParentId = 213213123
            };
            var sut = new AddAccountGroupCommandHandler(Context);
            var ex = await Assert.ThrowsAsync<Exception>(async () => await sut.Handle(query));
            ex.Message.Should().Be("Parent organization does not exist");
        }

        [Fact]
        public async void ReturnErrorWhenAccountGroupAlreadyExistsUnderParent()
        {
            var guid = new Guid().ToString();
            var organization = new Organization() { Name = guid, Code = guid };
            Context.Organizations.Add(organization);
            var accountGroups = new List<AccountGroup>
            {
                new AccountGroup(){Name = $"TG1{guid}", DisplayName = $"TG1{guid}", IsPrimary = true, ParentId = 0, OrganizationId = organization.Id},
                new AccountGroup(){Name = $"TG2{guid}", DisplayName =$"TG2{guid}", IsPrimary = true, ParentId = 0, OrganizationId = organization.Id},
            };
            await Context.AccountGroups.AddRangeAsync(accountGroups);
            Context.SaveChanges();
            var query = new AddAccountGroupCommand
            {
                DisplayName = $"TG1{guid}",
                Name = $"TG1{guid}",
                IsPrimary = true,
                ParentId = 0,
                OrganizationId = organization.Id
            };
            var sut = new AddAccountGroupCommandHandler(Context);
            var ex = await Assert.ThrowsAsync<Exception>(async () => await sut.Handle(query));
            ex.Message.Should().Be("Account Group with same name already exists under this parent");
        }
    }
}
