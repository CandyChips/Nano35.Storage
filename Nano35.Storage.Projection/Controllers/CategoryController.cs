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
    public class CategoryController :
        ControllerBase
    {
        private readonly IServiceProvider _services;
        
        public CategoryController(IServiceProvider services) { _services = services; }
        
        [HttpGet]
        [Route("GetAllCategories")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PresentationGetAllCategoriesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(PresentationGetAllCategoriesErrorHttpResponse))] 
        public async Task<IActionResult> GetAllCategories(
            [FromQuery] PresentationGetAllCategoriesHttpQuery query)
        {
            return await new CanonicalizedPresentationGetAllCategoriesRequestRequest(
                    new LoggedPipeNode<IPresentationGetAllCategoriesRequestContract, IPresentationGetAllCategoriesResultContract>(
                        _services.GetService(typeof(ILogger<IPresentationGetAllCategoriesRequestContract>)) as ILogger<IPresentationGetAllCategoriesRequestContract>,
                        new ValidatedPipeNode<IPresentationGetAllCategoriesRequestContract, IPresentationGetAllCategoriesResultContract>(
                            _services.GetService(typeof(IValidator<IPresentationGetAllCategoriesRequestContract>)) as IValidator<IPresentationGetAllCategoriesRequestContract>,
                            new PresentationGetAllCategoriesUseCase(
                                _services.GetService(typeof(IBus)) as IBus))))
                .Ask(query);
        }
    }
}