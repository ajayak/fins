using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FINS.Features.Accounting.AccountGroups.Operations;
using FINS.Models.Accounting;
using FluentAssertions;
using Xunit;

namespace FINS.UnitTest.Features.Accounting.AccountGroups.Operations
{
    [Collection("TestData")]
    public class AccountGroupExistsInOrganizationQueryHandlerShould : InMemoryContextTest
    {
        public int OrganizationId { get; set; }
        [Fact]
        public async void ReturnTrueWhenAccountGroupExists()
        {
            var sut = new AccountGroupExistsInOrganizationQueryHandler(Context);
            var query = new AccountGroupExistsInOrganizationQuery
            {
                OrganizationId = OrganizationId,
                AccountGroupName = "TestAGName",
                ParentAccountGroupId = 0
            };
            var result = await sut.Handle(query);
            result.Should().BeTrue();
        }

        [Fact]
        public async void ReturnFalseWhenAccountGroupNotExists()
        {
            var sut = new AccountGroupExistsInOrganizationQueryHandler(Context);
            var query = new AccountGroupExistsInOrganizationQuery
            {
                OrganizationId = 1000,
                AccountGroupName = "not_loan",
                ParentAccountGroupId = 0
            };
            var result = await sut.Handle(query);
            result.Should().BeFalse();
        }

        protected override void LoadTestData()
        {
            OrganizationId = Context.Organizations.Select(c => c.Id).FirstOrDefault();

            var accountGroup = new AccountGroup()
            {
                OrganizationId = OrganizationId,
                Name = "TestAGName",
                DisplayName = "TestAGName",
                ParentId = 0
            };
            Context.AccountGroups.Add(accountGroup);
            Context.SaveChanges();
        }
    }
}
