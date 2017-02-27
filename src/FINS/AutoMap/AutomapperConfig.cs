using AutoMapper;
using FINS.Features.Accounting.AccountGroups;
using FINS.Features.Accounting.AccountGroups.Operations;
using FINS.Models.Accounting;

namespace FINS.AutoMap
{
    public static class AutomapperConfig
    {
        public static void InitializeAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ToDTO>();
                cfg.AddProfile<FromDTO>();

                cfg.CreateMap<AccountGroupDto, AddAccountGroupCommand>();
                cfg.CreateMap<AddAccountGroupCommand, AccountGroupDto>();

                cfg.CreateMap<AccountGroupDto, UpdateAccountGroupCommand>();
                cfg.CreateMap<UpdateAccountGroupCommand, AccountGroupDto>();

                cfg.CreateMap<AddAccountGroupCommand, AccountGroup>();
                cfg.CreateMap<AccountGroup, AddAccountGroupCommand>();
            });
        }
    }
}
