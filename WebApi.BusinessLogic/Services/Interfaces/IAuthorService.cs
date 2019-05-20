using System.Threading.Tasks;
using WebApi.ViewModels.Responses.Author;

namespace WebApi.BusinessLogic.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<GetAuthorResponseViewModel> GetAuthor(long authorId);
        Task<GetAuthorListAuthorResponseViewModel> GetAuthorList();
        Task Delete(long id);
    }
}
