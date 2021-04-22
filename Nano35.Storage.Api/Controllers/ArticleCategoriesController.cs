using System;
using System.Threading.Tasks;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.storage;
using Nano35.Storage.Api.Requests;
using Nano35.Storage.Api.Requests.GetAllArticleCategories;

namespace Nano35.Storage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleCategoriesController :
        ControllerBase
    {
        private readonly IServiceProvider _services;

        public ArticleCategoriesController(
            IServiceProvider services)
        {
            _services = services;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllArticleCategoriesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllArticleCategoriesErrorHttpResponse))]
        public async Task<IActionResult> GetAllArticleCategories(
            [FromQuery] GetAllArticlesCategoriesHttpQuery query) =>
            await new ConvertedGetAllArticleCategoriesOnHttpContext(
                    new LoggedPipeNode<IGetAllArticlesCategoriesRequestContract, IGetAllArticlesCategoriesResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllArticlesCategoriesRequestContract>)) as ILogger<IGetAllArticlesCategoriesRequestContract>,
                        new GetAllArticleCategoriesUseCase(
                            _services.GetService(typeof(IBus)) as IBus)))
                .Ask(query);
    }
}