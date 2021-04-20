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
using Nano35.Storage.Api.Requests.GetAllArticleModels;

namespace Nano35.Storage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleModelsController :
        ControllerBase
    {
        private readonly IServiceProvider _services;

        public ArticleModelsController(
            IServiceProvider services)
        {
            _services = services;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllArticleModelsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllArticleModelsErrorHttpResponse))]
        public async Task<IActionResult> GetAllArticleModels(
            [FromQuery] GetAllArticleModelsHttpQuery query)
        {
            return await
                new ValidatedPipeNode<GetAllArticleModelsHttpQuery, IActionResult>(
                        _services.GetService(typeof(IValidator<GetAllArticleModelsHttpQuery>)) as
                            IValidator<GetAllArticleModelsHttpQuery>,
                        new ConvertedGetAllArticleModelsOnHttpContext(
                            new LoggedPipeNode<IGetAllArticlesModelsRequestContract, IGetAllArticlesModelsResultContract
                            >(
                                _services.GetService(typeof(ILogger<IGetAllArticlesModelsRequestContract>)) as
                                    ILogger<IGetAllArticlesModelsRequestContract>,
                                new GetAllArticlesModelsUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(query);
        }
    }
}

