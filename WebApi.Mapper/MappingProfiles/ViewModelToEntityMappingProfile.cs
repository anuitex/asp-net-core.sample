using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using WebApi.DataAccess.Entities;
using WebApi.ViewModels.Accounts;

namespace WebApi.Mapper.MappingProfiles
{
    public class ViewModelToEntityMappingProfile: Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            CreateMap<RegistrationViewModel, User>()
                .ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
        }
    }
}
