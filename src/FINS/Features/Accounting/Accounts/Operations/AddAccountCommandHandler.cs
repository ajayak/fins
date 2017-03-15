using System.Collections.Generic;
using System.Threading.Tasks;
using FINS.Context;
using FINS.Core.AutoMap;
using FINS.Features.Accounting.Accounts.DTO;
using FINS.Models.Accounting;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            await _context.Accounts.AddAsync(account);
            foreach (var person in account.ContactPersons)
            {
                person.AccountId = account.Id;
                _context.Entry(person).State = EntityState.Added;
                await _context.Persons.AddAsync(person);
            }
            await _context.SaveChangesAsync();
            return account.MapTo<AccountDto>();
        }
    }
}
