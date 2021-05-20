using System;
using System.Threading.Tasks;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.storage;
using Nano35.Storage.Projection.UseCases;
using Nano35.Storage.Projection.UseCases.PresentationGetAllArticles;

namespace Nano35.Storage.Projection.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly IServiceProvider _services;
        public ArticlesController(IServiceProvider services) => _services = services;
        
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PresentationGetAllArticlesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(PresentationGetAllArticlesErrorHttpResponse))] 
        public async Task<IActionResult> GetAllArticles(Guid instanceId)
        {
            var result = await new LoggedPipeNode<IPresentationGetAllArticlesRequestContract, IPresentationGetAllArticlesResultContract>(
                    _services.GetService(typeof(ILogger<IPresentationGetAllArticlesRequestContract>)) as ILogger<IPresentationGetAllArticlesRequestContract>,
                    new PresentationGetAllArticles(_services.GetService(typeof(IBus)) as IBus))
                .Ask(new PresentationGetAllArticlesRequestContract() { InstanceId = instanceId });
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
    }
}


