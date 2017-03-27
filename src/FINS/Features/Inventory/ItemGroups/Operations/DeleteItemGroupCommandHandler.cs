using System;
using System.Threading.Tasks;
using FINS.Context;
using FINS.Core.FinsExceptions;
using FINS.Models.Accounting;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FINS.Features.Inventory.ItemGroups.Operations
{
    public class DeleteItemGroupCommandHandler : IAsyncRequestHandler<DeleteItemGroupCommand, bool>
    {
        private readonly FinsDbContext _context;

        public DeleteItemGroupCommandHandler(FinsDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteItemGroupCommand message)
        {
            if (await ItemGroupIsParent(message.ItemGroupId))
            {
                throw new FinsInvalidOperation("Item group has related child item groups.");
            }
            if (await ItemGroupHasItems(message.ItemGroupId))
            {
                throw new FinsInvalidOperation("Item group has related active items.");
            }
            var itemGroup = await _context.ItemGroups.FindAsync(message.ItemGroupId);
            if (itemGroup == null)
            {
                throw new FinsNotFoundException("No matching Item group found.");
            }
            itemGroup.IsDeleted = true;
            itemGroup.IsPrimary = false;
            await CheckAndMakeItemGroupParentPrimary(itemGroup.ParentId, itemGroup.Id);
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<bool> ItemGroupIsParent(int itemGroupId)
        {
            return await _context.ItemGroups
                .AnyAsync(c => c.ParentId == itemGroupId && !c.IsDeleted);
        }

        private async Task<bool> ItemGroupHasItems(int itemGroupId)
        {
            return await _context.ItemGroups
                .AnyAsync(c => c.Id == itemGroupId && !c.IsDeleted);
        }

        private async Task CheckAndMakeItemGroupParentPrimary(int itemGroupParentId, int itemGroupId)
        {
            var itemGroupSiblings = await _context.ItemGroups
                .AnyAsync(c => c.ParentId == itemGroupParentId &&
                               c.Id != itemGroupId && !c.IsDeleted);
            if (itemGroupSiblings) return;
            var itemGroupParent = await _context.ItemGroups.FindAsync(itemGroupParentId);
            itemGroupParent.IsPrimary = true;
        }
    }
}
