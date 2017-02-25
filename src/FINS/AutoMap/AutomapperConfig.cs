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

                cfg.CreateMap<AccountGroupDto, AddAccountGroupQuery>();
                cfg.CreateMap<AddAccountGroupQuery, AccountGroupDto>();

                cfg.CreateMap<AddAccountGroupQuery, AccountGroup>();
                cfg.CreateMap<AccountGroup, AddAccountGroupQuery>();
            });
        }
    }
}
