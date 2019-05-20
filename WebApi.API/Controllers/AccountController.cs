using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.API.Models;
using WebApi.BusinessLogic.Services.Interfaces;
using WebApi.ViewModels.Requests;
using WebApi.ViewModels.Requests.Account;
using WebApi.ViewModels.Responses;
using WebApi.ViewModels.Responses.Account;

namespace WebApi.API.Controllers
{
    [Authorize(Policy = "Bearer")]
    public class AccountController: ControllerBase
    {
        private readonly IAccountsService _accountsService;
        private readonly AccountLite _account;

        public AccountController(IAccountsService accountsService, AccountLite account)
        {
            _accountsService = accountsService;
            _account = account;
        }

        /// <summary>
        /// Retrieves authorized user's account
        /// </summary>
        /// <returns>Account of authorized user</returns>
        [HttpGet("api/account")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        public async Task<ActionResult<GetAccountResponseViewModel>> Get()
        {
            GetAccountResponseViewModel getAccountResponseViewModel = await _accountsService.Get(_account.Id);
            return Ok(getAccountResponseViewModel);
        }

        /// <summary>
        /// Creates a new account
        /// </summary>
        /// <param name="createAccountRequestViewModel"></param>
        /// <returns>Created account's id</returns>
        [AllowAnonymous]
        [HttpPost("api/account/create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<CreateAccountResponseViewModel>> Create([FromBody] CreateAccountRequestViewModel createAccountRequestViewModel)
        {
            CreateAccountResponseViewModel createAccountResponseViewModel = await _accountsService.Create(createAccountRequestViewModel);
            return Ok(createAccountResponseViewModel);
        }

        /// <summary>
        /// Updates authorized user's account
        /// </summary>
        /// <param name="updateAccountRequestViewModel"></param>
        /// <returns></returns>
        [HttpPost("api/account/update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Consumes("application/json")]
        public async Task<ActionResult> Update([FromBody] UpdateAccountRequestViewModel updateAccountRequestViewModel)
        {
            await _accountsService.Update(_account.Id, updateAccountRequestViewModel);
            return Ok();
        }

        /// <summary>
        /// Deletes authorized user's account
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/account/delete")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult> Delete()
        {
            await _accountsService.Delete(_account.Id);
            return Ok();
        }

        /// <summary>
        /// Generates access token and refresh token for provided credentials
        /// </summary>
        /// <param name="signInAccountRequestViewModel"></param>
        /// <returns>Access token, refresh token, account id</returns>
        [AllowAnonymous]
        [HttpPost("api/account/sign-in")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<SignInAccountResponseViewModel>> SignIn([FromBody]SignInAccountRequestViewModel signInAccountRequestViewModel)
        {
            SignInAccountResponseViewModel token = await _accountsService.SignIn(signInAccountRequestViewModel);
            return Ok(token);
        }

        /// <summary>
        /// Refreshes current access token with provided refresh token
        /// </summary>
        /// <param name="refreshTokenAccountRequestViewModel"></param>
        /// <returns>Access token, refresh token, account id</returns>
        [AllowAnonymous]
        [HttpPost("api/account/refresh-token")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<RefreshTokenAccountResponseViewModel>> RefreshToken([FromBody]RefreshTokenAccountRequestViewModel refreshTokenAccountRequestViewModel)
        {
            RefreshTokenAccountResponseViewModel token = await _accountsService.RefreshToken(refreshTokenAccountRequestViewModel);
            return Ok(token);
        }

        /// <summary>
        /// Changes authorized user's password with provided old password and new password
        /// </summary>
        /// <param name="changePasswordAccountRequestViewModel"></param>
        /// <returns></returns>
        [HttpPost("api/account/change-password")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Consumes("application/json")]
        public async Task<ActionResult> ChangePassword([FromBody]ChangePasswordAccountRequestViewModel changePasswordAccountRequestViewModel)
        {
            await _accountsService.ChangePassword(_account.Id, changePasswordAccountRequestViewModel);
            return Ok();
        }
    }
}