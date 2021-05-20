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
using Nano35.Storage.Projection.UseCases.PresentationGetAllCategories;

namespace Nano35.Storage.Projection.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IServiceProvider _services;
        public CategoryController(IServiceProvider services) => _services = services;

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PresentationGetAllCategoriesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(PresentationGetAllCategoriesErrorHttpResponse))] 
        public async Task<IActionResult> GetAllCategories(Guid instanceId)
        {
            var result =
                await new LoggedPipeNode<IPresentationGetAllCategoriesRequestContract, IPresentationGetAllCategoriesResultContract>(
                    _services.GetService(typeof(ILogger<IPresentationGetAllCategoriesRequestContract>)) as ILogger<IPresentationGetAllCategoriesRequestContract>,
                    new PresentationGetAllCategories(_services.GetService(typeof(IBus)) as IBus))
                .Ask(new PresentationGetAllCategoriesRequestContract() { InstanceId = instanceId });
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);

        }
    }
}