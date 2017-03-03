using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FINS.Features.Accounting.Accounts.Operations;
using FluentAssertions;
using Xunit;

namespace FINS.UnitTest.Features.Accounting.Accounts.Operations
{
    [Collection("TestData")]
    public class GetAllAccountQueryHandlerShould : InMemoryContextTest
    {
        [Fact]
        public async void ReturnAccountsDtoPagedList()
        {
            var organizationId = Context.Organizations.Where(c => c.Name == "fs").Select(c => c.Id).FirstOrDefault();
            var query = new GetAllAccountQuery
            {
                OrganizationId = organizationId,
                PageSize = 1,
                PageNo = 1
            };
            var sut = new GetAllAccountQueryHandler(Context);
            var result = await sut.Handle(query);
            result.PageNo.Should().Be(1);
            result.PageSize.Should().Be(1);
            result.Items.Count.Should().Be(1);
        }
    }
}
