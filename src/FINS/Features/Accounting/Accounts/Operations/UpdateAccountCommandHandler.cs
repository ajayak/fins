using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FINS.Context;
using FINS.Core.AutoMap;
using FINS.Features.Accounting.Accounts.DTO;
using FINS.Models.Accounting;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FINS.Features.Accounting.Accounts.Operations
{
    public class UpdateAccountCommandHandler : IAsyncRequestHandler<UpdateAccountCommand, AccountDto>
    {
        private readonly FinsDbContext _context;

        public UpdateAccountCommandHandler(FinsDbContext context)
        {
            _context = context;
        }

        public async Task<AccountDto> Handle(UpdateAccountCommand query)
        {
            var account = query.MapTo<Account>();
            _context.Accounts.Attach(account);
            _context.Entry(account).State = EntityState.Modified;
            foreach (var person in account.ContactPersons)
            {
                person.AccountId = account.Id;
                if (person.Id < 1)
                {
                    _context.Entry(person).State = EntityState.Added;
                    await _context.Persons.AddAsync(person);
                }
                else
                {
                    _context.Persons.Attach(person);
                    _context.Entry(person).State = EntityState.Modified;
                }
            }
            var newPersonIds = account.ContactPersons.Select(c => c.Id).ToArray();

            await DeleteOrphanPersonRecords(account.Id, newPersonIds);
            await _context.SaveChangesAsync();
            return account.MapTo<AccountDto>();
        }

        private async Task DeleteOrphanPersonRecords(int accountId, int[] newPersonIds)
        {
            var orphanRecords = await _context.Persons
                .Where(c => c.AccountId == accountId && !newPersonIds.Contains(c.Id))
                .ToListAsync();
            _context.Persons.RemoveRange(orphanRecords);
        }
    }
}
