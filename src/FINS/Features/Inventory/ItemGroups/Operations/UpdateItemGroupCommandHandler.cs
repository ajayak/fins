using System;
using System.Threading.Tasks;
using FINS.Context;
using FINS.Core.AutoMap;
using FINS.Core.FinsExceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FINS.Features.Inventory.ItemGroups.Operations
{
    public class UpdateItemGroupCommandHandler
        : IAsyncRequestHandler<UpdateItemGroupCommand, ItemGroupDto>
    {
        private readonly FinsDbContext _context;

        public UpdateItemGroupCommandHandler(FinsDbContext context)
        {
            _context = context;
        }

        public async Task<ItemGroupDto> Handle(UpdateItemGroupCommand message)
        {
            var itemGroup = await _context.ItemGroups.FindAsync(message.Id);
            if (itemGroup == null)
            {
                throw new FinsNotFoundException("Item group does not exist");
            }
            if (itemGroup.ParentId != message.ParentId)
            {
                throw new FinsInvalidDataException("Cannot update Parent Id");
            }
            if (await ItemGroupExistsInOrganization(message, itemGroup.Name))
            {
                throw new FinsInvalidDataException("Item Group with same name already exists under this parent");
            }
            itemGroup.DisplayName = message.DisplayName;
            itemGroup.Name = message.Name;
            await _context.SaveChangesAsync();
            return itemGroup.MapTo<ItemGroupDto>();
        }

        private async Task<bool> ItemGroupExistsInOrganization
            (AddItemGroupCommand message, string oldName)
        {
            return await _context.ItemGroups
                .AnyAsync(c => c.ParentId == message.ParentId &&
                               c.OrganizationId == message.OrganizationId &&
                               !c.IsDeleted &&
                               !c.Name.Equals(oldName, StringComparison.OrdinalIgnoreCase) &&
                               c.Name.Equals(message.Name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
