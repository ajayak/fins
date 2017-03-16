using System;
using System.Linq;
using System.Threading.Tasks;
using FINS.Context;
using FINS.Core.AutoMap;
using FINS.Core.FinsExceptions;
using FINS.Models.Inventory;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FINS.Features.Inventory.ItemGroups.Operations
{
    public class AddItemGroupCommandHandler : IAsyncRequestHandler<AddItemGroupCommand, ItemGroupDto>
    {
        private readonly FinsDbContext _context;

        public AddItemGroupCommandHandler(FinsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add new Item group
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<ItemGroupDto> Handle(AddItemGroupCommand message)
        {
            if (!CheckParentOrganizationIdExists(message.ParentId))
            {
                throw new FinsInvalidDataException("Parent organization does not exist");
            }
            if (message.ParentId != 0)
            {
                var parentItemGroup = await _context.ItemGroups.FindAsync(message.ParentId);
                parentItemGroup.IsPrimary = false;
            }
            if (await ItemGroupExistsInOrganization(message))
            {
                throw new FinsInvalidOperation("Item Group with same name already exists under this parent");
            }

            var itemGroup = message.MapTo<ItemGroup>();
            itemGroup.IsPrimary = true;
            await _context.ItemGroups.AddAsync(itemGroup);

            await _context.SaveChangesAsync();
            return itemGroup.MapTo<ItemGroupDto>();
        }

        private bool CheckParentOrganizationIdExists(int parentId)
        {
            return parentId == 0 || _context.ItemGroups.Any(c => c.Id == parentId);
        }

        private async Task<bool> ItemGroupExistsInOrganization(AddItemGroupCommand message)
        {
            return await _context.ItemGroups
                .AnyAsync(c => c.ParentId == message.ParentId &&
                               c.OrganizationId == message.OrganizationId &&
                               !c.IsDeleted &&
                               c.Name.Equals(message.Name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
