using AutoMapper;
using WebApi.DataAccess.Entities;
using WebApi.ViewModels.Responses.Account;
using WebApi.ViewModels.Responses.Author;

namespace WebApi.BusinessLogic.MappingProfiles
{
    public class EntityToViewModelMappingProfile: Profile
    {
        public EntityToViewModelMappingProfile()
        {
            CreateMap<Account, CreateAccountResponseViewModel>()
                .ForMember(vm => vm.AccountId, map => map.MapFrom(e => e.Id));

            CreateMap<Account, GetAccountResponseViewModel>();

            CreateMap<Author, GetAuthorResponseViewModel>();

            CreateMap<Genre, GenreViewModelItem>();

            CreateMap<Author, AuthorViewModelItem>();
        }
    }
}
