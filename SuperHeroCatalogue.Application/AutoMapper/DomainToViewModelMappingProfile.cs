using AutoMapper;
using SuperHeroCatalogue.Application.Models;
using SuperHeroCatalogue.Domain.Entities;

namespace SuperHeroCatalogue.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName => "DomainToViewModelMappingProfile";

        protected override void Configure()
        {
            Mapper.CreateMap<User, UserModel>();
            Mapper.CreateMap<Role, RoleModel>();
            Mapper.CreateMap<SuperHero, SuperHeroModel>();
            Mapper.CreateMap<SuperPower, SuperPowerModel>();
            Mapper.CreateMap<ProtectionArea, ProtectionAreaModel>();
            Mapper.CreateMap<AuditEvent, AuditEventViewModel>();
        }
    }
}