using System;
using System.Linq;
using FINS.Core.FinsExceptions;
using FINS.Features.Accounting.AccountGroups.Operations;
using FINS.Models;
using FINS.Models.Accounting;
using FluentAssertions;
using Xunit;

namespace FINS.UnitTest.Features.Accounting.AccountGroups.Operations
{
    [Collection("TestData")]
    public class DeleteAccountGroupCommandHandlerShould : InMemoryContextTest
    {
        public int OrganizationId { get; set; }
        public int ParentAccountGroupId { get; set; }
        public int ChildAccountGroupId { get; set; }
        public int RelatedAccountGroupId { get; set; }

        [Fact]
        public async void ShouldDeleteChildAccountGroup()
        {
            var query = new DeleteAccountGroupCommand
            {
                OrganizationId = OrganizationId,
                AccountGroupId = ChildAccountGroupId
            };
            var sut = new DeleteAccountGroupCommandHandler(Context);
            var result = await sut.Handle(query);
            result.Should().BeTrue();
        }

        [Fact]
        public async void ShouldNotDeleteParentAccountGroup()
        {
            var query = new DeleteAccountGroupCommand
            {
                OrganizationId = OrganizationId,
                AccountGroupId = ParentAccountGroupId
            };
            var sut = new DeleteAccountGroupCommandHandler(Context);
            var ex = await Assert.ThrowsAsync<FinsInvalidOperation>(async () => await sut.Handle(query));
            ex.Message.Should().Be("Account group has related child account groups.");
        }

        [Fact]
        public async void ShouldNotDeleteAccountGroupWithChildAccount()
        {
            var query = new DeleteAccountGroupCommand
            {
                OrganizationId = OrganizationId,
                AccountGroupId = RelatedAccountGroupId
            };
            var sut = new DeleteAccountGroupCommandHandler(Context);
            var ex = await Assert.ThrowsAsync<FinsInvalidOperation>(async () => await sut.Handle(query));
            ex.Message.Should().Be("Account group has related active accounts.");
        }

        [Fact]
        public async void ShoulDeleteOnlyChildAccountGroupAndMakeParentPrimary()
        {
            var query = new DeleteAccountGroupCommand
            {
                OrganizationId = OrganizationId,
                AccountGroupId = ChildAccountGroupId
            };
            var sut = new DeleteAccountGroupCommandHandler(Context);
            await sut.Handle(query);
            var parent = await Context.AccountGroups.FindAsync(ParentAccountGroupId);
            parent.IsPrimary.Should().BeTrue();
        }

        [Fact]
        public async void ShouldNotDeleteAccountGroupWhenNotExists()
        {
            var query = new DeleteAccountGroupCommand
            {
                OrganizationId = OrganizationId,
                AccountGroupId = 78123672
            };
            var sut = new DeleteAccountGroupCommandHandler(Context);
            var ex = await Assert.ThrowsAsync<FinsNotFoundException>(async () => await sut.Handle(query));
            ex.Message.Should().Be("No matching Account group found.");
        }

        protected override void LoadTestData()
        {
            var guid = new Guid().ToString();
            OrganizationId = Context.Organizations.Select(c => c.Id).FirstOrDefault();
            var accountGroup = new AccountGroup()
            {
                OrganizationId = OrganizationId,
                Name = guid,
                DisplayName = guid,
                ParentId = 0
            };
            Context.AccountGroups.Add(accountGroup);
            var childAccountGroup = new AccountGroup()
            {
                OrganizationId = OrganizationId,
                Name = $"child{guid}",
                DisplayName = $"child{guid}",
                ParentId = accountGroup.Id,
                IsPrimary = true
            };
            Context.AccountGroups.Add(childAccountGroup);
            Context.SaveChanges();
            ParentAccountGroupId = accountGroup.Id;
            ChildAccountGroupId = childAccountGroup.Id;

            var account = new Account()
            {
                AccountGroup = new AccountGroup()
                {
                    OrganizationId = OrganizationId,
                    DisplayName = "A13223SD",
                    IsPrimary = true,
                    Name = "AK",
                    ParentId = 0
                },
                DisplayName = "asd",
                Name = "Asd",
                OpeningBalance = 1,
                OpeningBalanceType = TransactionType.Credit
            };
            Context.Accounts.Add(account);
            Context.SaveChanges();
            RelatedAccountGroupId = account.AccountGroupId;
        }
    }
}
