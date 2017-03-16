using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FINS.Features.Accounting.AccountGroups.Operations;
using FINS.Models.Accounting;
using FINS.Models.App;
using FluentAssertions;
using Xunit;

namespace FINS.UnitTest.Features.Accounting.AccountGroups.Operations
{
    [Collection("TestData")]
    public class GetAccountGroupDictionaryQueryHandlerShould : InMemoryContextTest
    {
        public int OrganizationId { get; set; }

        [Fact]
        public async void ReturnsAccountGroupsDictionaryForOrganization()
        {
            var sut = new GetPrimaryAccountGroupDictionaryQueryHandler(Context);
            var result = await sut.Handle(new GetPrimaryAccountGroupDictionaryQuery() { OrganizationId = OrganizationId });
            result.Should().BeOfType<Dictionary<int, string>>();
            result.Keys.Count.Should().Be(4);
            result.Values.Should().Contain("TG3");
        }

        protected override void LoadTestData()
        {
            var organization = new Organization() { Name = "TO", Code = "TO" };
            Context.Organizations.Add(organization);
            Context.SaveChanges();
            OrganizationId = organization.Id;
            var accountGroups = new List<AccountGroup>
            {
                new AccountGroup(){Name = "TG1", DisplayName = "TG1", IsPrimary = true, ParentId = 0, OrganizationId = OrganizationId},
                new AccountGroup(){Name = "TG2", DisplayName = "TG2", IsPrimary = true, ParentId = 0, OrganizationId = OrganizationId},
                new AccountGroup(){Name = "TG3", DisplayName = "TG3", IsPrimary = true, ParentId = 0, OrganizationId = OrganizationId}
            };
            Context.AccountGroups.AddRange(accountGroups);
            Context.SaveChanges();
            var childAccountGroup = new AccountGroup()
            {
                Name = "TG3_Child",
                DisplayName = "TG3_Child",
                IsPrimary = true,
                ParentId = accountGroups[2].Id,
                OrganizationId = OrganizationId
            };
            Context.AccountGroups.Add(childAccountGroup);
            Context.SaveChanges();
        }
    }
}
