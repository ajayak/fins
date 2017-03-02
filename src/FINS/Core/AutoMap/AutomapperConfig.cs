using AutoMapper;
using FINS.Features.Accounting.AccountGroups;
using FINS.Features.Accounting.AccountGroups.Operations;
using FINS.Features.Accounting.Accounts;
using FINS.Models.Accounting;

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

                cfg.CreateMap<AccountDto, Account>().ReverseMap();
            });
        }
    }
}
