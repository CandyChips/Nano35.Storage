using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.storage;
using Nano35.Storage.Api.Requests;
using Nano35.Storage.Api.Requests.CreateArticle;
using Nano35.Storage.Api.Requests.GetAllArticleBrands;
using Nano35.Storage.Api.Requests.GetAllArticleBrands;
using Nano35.Storage.Api.Requests.GetAllArticles;
using Nano35.Storage.Api.Requests.GetArticleById;
using Nano35.Storage.Api.Requests.UpdateArticleBrand;
using Nano35.Storage.Api.Requests.UpdateArticleCategory;
using Nano35.Storage.Api.Requests.UpdateArticleInfo;
using Nano35.Storage.Api.Requests.UpdateArticleBrand;

namespace Nano35.Storage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleBrandsController :
        ControllerBase
    {
        private readonly IServiceProvider _services;
        public ArticleBrandsController(IServiceProvider services) { _services = services; }
        
        [HttpPatch]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateArticleBrandSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateArticleBrandErrorHttpResponse))] 
        public async Task<IActionResult> UpdateArticleBrand([FromBody] UpdateArticleBrandHttpBody body)
        {
            return await new CanonicalizedUpdateArticleBrandRequest(
                new LoggedPipeNode<IUpdateArticleBrandRequestContract, IUpdateArticleBrandResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateArticleBrandRequestContract>)) as ILogger<IUpdateArticleBrandRequestContract>,  
                    new ValidatedPipeNode<IUpdateArticleBrandRequestContract, IUpdateArticleBrandResultContract>(
                        _services.GetService(typeof(IValidator<IUpdateArticleBrandRequestContract>)) as IValidator<IUpdateArticleBrandRequestContract>,
                        new UpdateArticleBrandUseCase(
                            _services.GetService(typeof(IBus)) as IBus)))).Ask(body);
        }
    }
}