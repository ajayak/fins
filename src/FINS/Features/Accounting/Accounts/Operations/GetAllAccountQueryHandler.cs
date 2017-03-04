﻿using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FINS.Context;
using FINS.Core.AutoMap;
using FINS.Core.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FINS.Features.Accounting.Accounts.Operations
{
    public class GetAllAccountQueryHandler : IAsyncRequestHandler<GetAllAccountQuery, PagedResult<AccountDto>>
    {
        private readonly FinsDbContext _context;

        public GetAllAccountQueryHandler(FinsDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<AccountDto>> Handle(GetAllAccountQuery message)
        {
            var query = _context.Accounts
                .Where(c =>
                c.AccountGroup.OrganizationId == message.OrganizationId &&
                !c.AccountGroup.IsDeleted &&
                !c.IsDeleted);

            var totalRecordCount = await query.CountAsync();
            var result = await query
                .ApplySort(message.Sort)
                .ApplyPaging(message.PageNo, message.PageSize)
                .Select(c => new AccountDto()
                {
                    Id = c.Id,
                    Name = c.Name,
                    DisplayName = c.DisplayName,
                    Code = c.Code,
                    AccountGroupName = c.AccountGroup.Name
                })
                .ToListAsync();

            return result.ToPagedResult(message.PageNo, message.PageSize, totalRecordCount);
        }
    }
}

