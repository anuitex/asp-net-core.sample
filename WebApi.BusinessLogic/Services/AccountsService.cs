using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebApi.BusinessLogic.Exceptions;
using WebApi.BusinessLogic.Extensions;
using WebApi.BusinessLogic.Hubs;
using WebApi.BusinessLogic.Services.Interfaces;
using WebApi.Config.Factories.Interfaces;
using WebApi.Config.Helpers;
using WebApi.Config.Models;
using WebApi.Config.Params;
using WebApi.DataAccess.Entities;
using WebApi.DataAccess.Repositories.Interfaces;
using WebApi.ViewModels.Requests.Account;
using WebApi.ViewModels.Responses.Account;

namespace WebApi.BusinessLogic.Services
{
    public class AccountsService: IAccountsService
    {
        private readonly UserManager<Account> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenFactory _tokenFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IHubContext<NotificationHub> _notificationHub;

        public AccountsService(
            UserManager<Account> userManager,
            IMapper mapper,
            ITokenFactory tokenFactory,
            IOptions<JwtIssuerOptions> jwtOptions,
            IRefreshTokenRepository refreshTokenRepository,
            IHubContext<NotificationHub> notificationHub)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenFactory = tokenFactory;
            _jwtOptions = jwtOptions.Value;
            _refreshTokenRepository = refreshTokenRepository;
            _notificationHub = notificationHub;
        }

        public async Task<CreateAccountResponseViewModel> Create(CreateAccountRequestViewModel model)
        {
            Account account = _mapper.Map<CreateAccountRequestViewModel, Account>(model);

            IdentityResult result = await _userManager.CreateAsync(account, model.Password);

            if (!result.Succeeded)
            {
                throw new IdentityException(result.Errors.GetErrors());
            }

            CreateAccountResponseViewModel createAccountResponseViewModel = _mapper.Map<Account, CreateAccountResponseViewModel>(account);

            await _notificationHub.Clients.All.SendAsync("clientSubscription", ($"{model.FirstName} {model.LastName} has joined our community!"));

            return createAccountResponseViewModel;
        }

        public async Task<GetAccountResponseViewModel> Get(long accountId)
        {
            Account account = await _userManager.FindByIdAsync(accountId.ToString());

            if (account == null)
            {
                throw new IdentityException($"Account with id {accountId} was not found");
            }

            GetAccountResponseViewModel getAccountResponseViewModel = _mapper.Map<Account, GetAccountResponseViewModel>(account);

            await _notificationHub.Clients.All.SendAsync("clientSubscription", ($"{getAccountResponseViewModel.FirstName} {getAccountResponseViewModel.LastName} has signed in!"));

            return getAccountResponseViewModel;
        }

        public async Task Update(long accountId, UpdateAccountRequestViewModel updateAccountRequestViewModel)
        {
            Account account = await _userManager.FindByIdAsync(accountId.ToString());

            _mapper.Map(updateAccountRequestViewModel, account);

            IdentityResult result = await _userManager.UpdateAsync(account);

            if (!result.Succeeded)
            {
                throw new IdentityException(result.Errors.GetErrors());
            }
        }

        public async Task ChangePassword(long accountId, ChangePasswordAccountRequestViewModel changePasswordRequestViewModel)
        {
            Account account = await _userManager.FindByIdAsync(accountId.ToString());

            IdentityResult result = await _userManager.ChangePasswordAsync(account, changePasswordRequestViewModel.CurrentPassword, changePasswordRequestViewModel.NewPassword);

            if (!result.Succeeded)
            {
                throw new IdentityException(result.Errors.GetErrors());
            }
        }

        public async Task Delete(long accountId)
        {
            Account account = await _userManager.FindByIdAsync(accountId.ToString());

            IdentityResult result = await _userManager.DeleteAsync(account);

            if (!result.Succeeded)
            {
                throw new IdentityException(result.Errors.GetErrors());
            }
        }

        public async Task<SignInAccountResponseViewModel> SignIn(SignInAccountRequestViewModel signInAccountRequestViewModel)
        {
            Account account = await GetAccount(signInAccountRequestViewModel.Email, signInAccountRequestViewModel.Password);

            if (account == null)
            {
                throw new IdentityException("Invalid username or password");
            }

            Token token = await GetToken(account.Id, account.Email);

            var signInAccountResponseViewModel = new SignInAccountResponseViewModel
            {
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken,
                AccessTokenExpirationDate = token.AccessTokenExpirationDate,
                RefreshTokenExpirationDate = token.RefreshTokenExpirationDate,
                AccountId = token.Id
            };

            return signInAccountResponseViewModel;
        }

        public async Task<RefreshTokenAccountResponseViewModel> RefreshToken(RefreshTokenAccountRequestViewModel refreshTokenAccountRequestViewModel)
        {
            RefreshToken currentRefreshToken = await _refreshTokenRepository.GetByTokenAsync(refreshTokenAccountRequestViewModel.RefreshToken);

            if (currentRefreshToken == null)
            {
                throw new IdentityException("Refresh token is not valid.");
            }

            if (currentRefreshToken.ExpiresUtc < DateTime.Now.ToUniversalTime())
            {
                throw new IdentityException("Refresh token has expired.");
            }

            Token token = await GetToken(currentRefreshToken.AccountId, currentRefreshToken.Account.Email);

            var refreshTokenAccountResponseViewModel = new RefreshTokenAccountResponseViewModel
            {
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken,
                AccessTokenExpirationDate = token.AccessTokenExpirationDate,
                RefreshTokenExpirationDate = token.RefreshTokenExpirationDate,
                AccountId = token.Id
            };

            return refreshTokenAccountResponseViewModel;
        }

        private async Task RevokeRefreshToken(long accountId)
        {
            RefreshToken token = await _refreshTokenRepository.GetByAccountIdAsync(accountId);

            if (token == null)
            {
                return;
            }

            await _refreshTokenRepository.RemoveAsync(token);
        }

        private async Task<RefreshToken> AttachRefreshToken(long accountId)
        {
            string token = _tokenFactory.GenerateEncodedRefreshToken();

            DateTime dateTimeNow = DateTime.Now;

            var refreshToken = new RefreshToken
            {
                AccountId = accountId,
                Token = token,
                IssuedUtc = dateTimeNow,
                ExpiresUtc = dateTimeNow.AddDays(14)
            };

            await _refreshTokenRepository.AddAsync(refreshToken);

            return refreshToken;
        }

        private async Task<Token> GenerateAccessToken(string userName, ClaimsIdentity identity)
        {
            Token token = await TokenHelper.GenerateJwt(new GenerateJwtParams
            {
                Identity = identity,
                TokenFactory = _tokenFactory,
                UserName = userName,
                JwtOptions = _jwtOptions,
                SerializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented }
            });

            return token;
        }

        private async Task<Token> GetToken(long accountId, string email)
        {
            await RevokeRefreshToken(accountId);

            RefreshToken refreshToken = await AttachRefreshToken(accountId);

            ClaimsIdentity identity = _tokenFactory.GenerateClaimsIdentity(email, accountId.ToString());

            Token token = await GenerateAccessToken(email, identity);

            token.RefreshToken = refreshToken.Token;
            token.RefreshTokenExpirationDate = refreshToken.ExpiresUtc;

            return token;
        }

        private async Task<Account> GetAccount(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return await Task.FromResult<Account>(null);
            }

            Account userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null)
            {
                return await Task.FromResult<Account>(null);
            }

            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(userToVerify);
            }

            return await Task.FromResult<Account>(null);
        }
    }
}
