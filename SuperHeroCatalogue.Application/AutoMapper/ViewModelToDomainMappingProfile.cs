using AutoMapper;
using SuperHeroCatalogue.Application.Models;
using SuperHeroCatalogue.Domain.Entities;

namespace SuperHeroCatalogue.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName => "ViewModelToDomainMappingProfile";

        protected override void Configure()
        {
            Mapper.CreateMap<UserModel, User>();
            Mapper.CreateMap<RoleModel, Role>();
            Mapper.CreateMap<SuperHeroModel, SuperHero>();
            Mapper.CreateMap<SuperPowerModel, SuperPower>();
            Mapper.CreateMap<ProtectionAreaModel, ProtectionArea>();
            Mapper.CreateMap<AuditEventViewModel, AuditEvent>();
        }
    }
}