using AutoMapper;
using WebApi.DataAccess.Entities;
using WebApi.ViewModels.Requests;
using WebApi.ViewModels.Requests.Account;

namespace WebApi.BusinessLogic.MappingProfiles
{
    public class ViewModelToEntityMappingProfile: Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            CreateMap<CreateAccountRequestViewModel, Account>()
                .ForMember(e => e.UserName, map => map.MapFrom(vm => vm.Email));

            CreateMap<UpdateAccountRequestViewModel, Account>();
        }
    }
}
