using System.Collections.Generic;
using System.Threading.Tasks;
using FINS.Context;
using FINS.Core.AutoMap;
using FINS.Features.Accounting.Accounts.DTO;
using FINS.Models.Accounting;
using MediatR;

namespace FINS.Features.Accounting.Accounts.Operations
{
    public class AddAccountCommandHandler : IAsyncRequestHandler<AddAccountCommand, AccountDto>
    {
        private readonly FinsDbContext _context;

        public AddAccountCommandHandler(FinsDbContext context)
        {
            _context = context;
        }

        public async Task<AccountDto> Handle(AddAccountCommand query)
        {
            var account = query.MapTo<Account>();
            foreach (var person in account.ContactPersons)
            {
                person.AccountId = account.Id;
            }

            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
            return account.MapTo<AccountDto>();
        }
    }
}
