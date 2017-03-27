using AutoMapper;
using FINS.Features.Accounting.AccountGroups;
using FINS.Features.Accounting.AccountGroups.Operations;
using FINS.Features.Accounting.Accounts.DTO;
using FINS.Features.Accounting.Accounts.Operations;
using FINS.Features.Inventory.ItemGroups;
using FINS.Features.Inventory.ItemGroups.Operations;
using FINS.Features.Inventory.Items.DTO;
using FINS.Features.Inventory.Items.Operations;
using FINS.Models.Accounting;
using FINS.Models.Inventory;

namespace FINS.Core.AutoMap
{
    public static class AutomapperConfig
    {
        public static void InitializeAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ToDTO>();
                cfg.AddProfile<FromDTO>();

                cfg.CreateMap<AccountGroupDto, AddAccountGroupCommand>().ReverseMap();
                cfg.CreateMap<AccountGroupDto, UpdateAccountGroupCommand>().ReverseMap();
                cfg.CreateMap<AddAccountGroupCommand, AccountGroup>().ReverseMap();

                cfg.CreateMap<AccountListDto, Account>().ReverseMap();
                cfg.CreateMap<AccountDto, Account>().ReverseMap();
                cfg.CreateMap<AccountDto, AddAccountCommand>().ReverseMap();
                cfg.CreateMap<AccountDto, UpdateAccountCommand>().ReverseMap();

                cfg.CreateMap<PersonDto, Person>().ReverseMap();

                cfg.CreateMap<ItemGroupDto, AddItemGroupCommand>().ReverseMap();
                cfg.CreateMap<ItemGroupDto, UpdateItemGroupCommand>().ReverseMap();
                cfg.CreateMap<AddItemGroupCommand, ItemGroup>().ReverseMap();

                cfg.CreateMap<Item, ItemListDto>()
                    .ForMember(c => c.ImageUrl, c => c.MapFrom(i => i.ImageName));
                cfg.CreateMap<ItemDto, Item>().ReverseMap();
                cfg.CreateMap<ItemDto, AddItemCommand>().ReverseMap();
                cfg.CreateMap<ItemDto, UpdateItemCommand>().ReverseMap();
            });
        }
    }
}
