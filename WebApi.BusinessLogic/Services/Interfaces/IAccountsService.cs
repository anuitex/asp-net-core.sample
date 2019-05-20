using System.Threading.Tasks;
using WebApi.ViewModels.Requests;
using WebApi.ViewModels.Requests.Account;
using WebApi.ViewModels.Responses;
using WebApi.ViewModels.Responses.Account;

namespace WebApi.BusinessLogic.Services.Interfaces
{
    public interface IAccountsService
    {
        Task<CreateAccountResponseViewModel> Create(CreateAccountRequestViewModel createAccountRequestViewModel);
        Task<GetAccountResponseViewModel> Get(long accountId);
        Task Update(long accountId, UpdateAccountRequestViewModel updateAccountRequestViewModel);
        Task Delete(long accountId);
        Task<SignInAccountResponseViewModel> SignIn(SignInAccountRequestViewModel signInAccountRequestViewModel);
        Task<RefreshTokenAccountResponseViewModel> RefreshToken(RefreshTokenAccountRequestViewModel refreshTokenAccountRequestViewModel);
        Task ChangePassword(long accountId, ChangePasswordAccountRequestViewModel changePasswordRequestViewModel);
    }
}
