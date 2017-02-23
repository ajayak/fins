using AutoMapper;

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
            });
        }
    }
}
