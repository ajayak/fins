using System;
using AutoMapper;
using AutoMapper.Configuration.Conventions;
using AutoMapper.Mappers;

namespace FINS.AutoMap
{
    // Flattens with NameSplitMember
    // Only applies to types that have same name with destination ending with Dto
    // Only applies Dto post fixes to the source properties
    public class ToDTO : Profile
    {
        [Obsolete("Create a constructor and configure inside of your profile\'s constructor instead. Will be removed in 6.0")]
        protected override void Configure()
        {
            AddMemberConfiguration().AddMember<NameSplitMember>().AddName<PrePostfixName>(
                    _ => _.AddStrings(p => p.Postfixes, "Dto"));
            AddConditionalObjectMapper().Where((s, d) => s.Name == d.Name + "Dto");
        }
    }

    // Doesn't Flatten Objects
    // Only applies to types that have same name with source ending with Dto
    // Only applies Dto post fixes to the destination properties
    public class FromDTO : Profile
    {
        [Obsolete("Create a constructor and configure inside of your profile\'s constructor instead. Will be removed in 6.0")]
        protected override void Configure()
        {
            AddMemberConfiguration().AddName<PrePostfixName>(
                    _ => _.AddStrings(p => p.DestinationPostfixes, "Dto"));
            AddConditionalObjectMapper().Where((s, d) => d.Name == s.Name + "Dto");
        }
    }
}
