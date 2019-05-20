using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.BusinessLogic.Services.Interfaces;
using WebApi.ViewModels.Responses.Author;

namespace WebApi.API.Controllers
{
    public class AuthorController: ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        /// <summary>
        /// Retrieves author by provided id, it's books and each book genres
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("api/author/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<GetAuthorResponseViewModel>> Get(long id)
        {
            GetAuthorResponseViewModel getAuthorResponseViewModel = await _authorService.GetAuthor(id);
            return Ok(getAuthorResponseViewModel);
        }

        /// <summary>
        /// Retrieves all authors
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/author/get-author-list")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Produces("application/json")]
        public async Task<ActionResult<GetAuthorListAuthorResponseViewModel>> GetAuthorList()
        {
            GetAuthorListAuthorResponseViewModel getAuthorListAccountResponseViewModel = await _authorService.GetAuthorList();
            return Ok(getAuthorListAccountResponseViewModel);
        }

        /// <summary>
        /// Deletes author by specified Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("api/author/delete/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult> Delete(long id)
        {
            await _authorService.Delete(id);
            return Ok();
        }
    }
}
