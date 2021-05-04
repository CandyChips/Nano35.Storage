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
using Nano35.Storage.Api.Requests.GetAllArticleBrands;
using Nano35.Storage.Api.Requests.GetAllArticles;

namespace Nano35.Storage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleBrandsController :
        ControllerBase
    {
        private readonly IServiceProvider _services;

        public ArticleBrandsController(
            IServiceProvider services)
        {
            _services = services;
        }


        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllArticleBrandsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllArticleBrandsErrorHttpResponse))]
        public async Task<IActionResult> GetAllArticleBrands(
            [FromQuery] GetAllArticlesBrandsHttpQuery query)
        {
            var result =
                await new LoggedUseCasePipeNode<IGetAllArticlesBrandsRequestContract, IGetAllArticlesBrandsResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllArticlesBrandsRequestContract>)) as
                            ILogger<IGetAllArticlesBrandsRequestContract>,
                        new GetAllArticleBrandsUseCase(_services.GetService(typeof(IBus)) as IBus))
                    .Ask(new GetAllArticlesBrandsRequestContract() {CategoryId = query.CategoryId});
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
    }
}