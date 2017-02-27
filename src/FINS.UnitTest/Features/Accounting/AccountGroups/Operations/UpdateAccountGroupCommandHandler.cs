using System;
using System.Collections.Generic;
using FINS.Features.Accounting.AccountGroups.Operations;
using FINS.Models.Accounting;
using FINS.Models.App;
using FluentAssertions;
using Xunit;

namespace FINS.UnitTest.Features.Accounting.AccountGroups.Operations
{
    [Collection("TestData")]
    public class UpdateAccountGroupCommandHandlerShould : InMemoryContextTest
    {
        public int AccountGroupId { get; set; }
        public int OrganizationId { get; set; }
        public string DuplicateName { get; set; }

        [Fact]
        public async void UpdateValidAccountGroup()
        {
            var query = new UpdateAccountGroupCommand
            {
                OrganizationId = OrganizationId,
                DisplayName = "UpdatedTest",
                Name = "UpdatedTest",
                IsPrimary = true,
                ParentId = 0,
                Id = AccountGroupId
            };
            var sut = new UpdateAccountGroupCommandHandler(Context);
            var result = await sut.Handle(query);
            result.DisplayName.Should().Be("UpdatedTest");
            result.Name.Should().Be("UpdatedTest");
        }

        [Fact]
        public async void ReturnErrorMessageWhenAccountGroupNotFount()
        {
            var query = new UpdateAccountGroupCommand
            {
                OrganizationId = OrganizationId,
                DisplayName = "UpdatedTest",
                Name = "UpdatedTest",
                IsPrimary = true,
                ParentId = 0,
                Id = 129736123
            };
            var sut = new UpdateAccountGroupCommandHandler(Context);
            var ex = await Assert.ThrowsAsync<Exception>(async () => await sut.Handle(query));
            ex.Message.Should().Be("Account group does not exist");
        }

        [Fact]
        public async void ReturnErrorWhenTryingToUpdateParentId()
        {
            var query = new UpdateAccountGroupCommand
            {
                OrganizationId = OrganizationId,
                DisplayName = "UpdatedTest",
                Name = "UpdatedTest",
                IsPrimary = true,
                ParentId = 1,
                Id = AccountGroupId
            };
            var sut = new UpdateAccountGroupCommandHandler(Context);
            var ex = await Assert.ThrowsAsync<Exception>(async () => await sut.Handle(query));
            ex.Message.Should().Be("Cannot update Parent Id");
        }

        [Fact]
        public async void ReturnErrorWhenTryingToUpdateSameNameExists()
        {
            var query = new UpdateAccountGroupCommand
            {
                OrganizationId = OrganizationId,
                DisplayName = "UpdatedTest",
                Name = DuplicateName,
                IsPrimary = true,
                ParentId = 0,
                Id = AccountGroupId
            };
            var sut = new UpdateAccountGroupCommandHandler(Context);
            var ex = await Assert.ThrowsAsync<Exception>(async () => await sut.Handle(query));
            ex.Message.Should().Be("Account Group with same name already exists under this parent");
        }

        protected override void LoadTestData()
        {
            var guid = new Guid().ToString();
            var organization = new Organization() { Name = guid, Code = guid };
            Context.Organizations.Add(organization);
            var accountGroups = new List<AccountGroup>
            {
                new AccountGroup(){Name = $"TG1{guid}", DisplayName = $"TG1{guid}", IsPrimary = true, ParentId = 0, OrganizationId = organization.Id},
                new AccountGroup(){Name = $"TG2{guid}", DisplayName =$"TG2{guid}", IsPrimary = true, ParentId = 0, OrganizationId = organization.Id},
            };
            Context.AccountGroups.AddRange(accountGroups);
            Context.SaveChanges();
            DuplicateName = $"TG2{guid}";
            OrganizationId = organization.Id;
            AccountGroupId = accountGroups[0].Id;
        }
    }
}
