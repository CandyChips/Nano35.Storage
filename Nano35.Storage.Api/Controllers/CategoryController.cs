using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.instance;
using Nano35.HttpContext.storage;
using Nano35.Storage.Api.Requests;
using Nano35.Storage.Api.Requests.CreateCategory;
using Nano35.Storage.Api.Requests.GetAllArticleCategories;
using Nano35.Storage.Api.Requests.UpdateCategoryName;
using Nano35.Storage.Api.Requests.UpdateCategoryParentCategoryId;

namespace Nano35.Storage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController :
        ControllerBase
    {
        private readonly IServiceProvider _services;
        
        public CategoryController(IServiceProvider services) { _services = services; }
    
        [HttpGet]
        [Route("GetAllArticleCategories")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllArticleCategoriesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllArticleCategoriesErrorHttpResponse))] 
        public async Task<IActionResult> GetAllArticleCategories(
            [FromQuery] GetAllArticlesCategoriesHttpQuery query)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<IGetAllArticlesCategoriesRequestContract>)_services.GetService(typeof(ILogger<IGetAllArticlesCategoriesRequestContract>));

            return await new CanonicalizedGetAllArticleCategoriesRequest(
                        new LoggedPipeNode<IGetAllArticlesCategoriesRequestContract, IGetAllArticlesCategoriesResultContract>(logger,
                            new ValidatedGetAllArticlesCategoriesRequest(
                                new GetAllArticleCategoriesUseCase(bus)))).Ask(query);
            
        }
        
        [HttpPost]
        [Route("CreateCategory")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateCategorySuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateCategoryErrorHttpResponse))] 
        public async Task<IActionResult> CreateCategory(
            [FromBody] CreateCategoryHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<ICreateCategoryRequestContract>)_services.GetService(typeof(ILogger<ICreateCategoryRequestContract>));

            return await new CanonicalizedCreateCategoryRequest(
                        new LoggedPipeNode<ICreateCategoryRequestContract, ICreateCategoryResultContract>(logger,
                            new ValidatedCreateCategoryRequest(
                                new CreateCategoryUseCase(bus)))).Ask(body);
        }
        
        [HttpPatch]
        [Route("UpdateCategoryName")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateCategoryNameSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateCategoryNameErrorHttpResponse))] 
        public async Task<IActionResult> UpdateCategoryName(
            [FromBody] UpdateCategoryNameHttpBody body)
        {
            
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<IUpdateCategoryNameRequestContract>) _services.GetService(typeof(ILogger<IUpdateCategoryNameRequestContract>));

            return await new CanonicalizedUpdateCategoryNameRequest(
                        new LoggedPipeNode<IUpdateCategoryNameRequestContract, IUpdateCategoryNameResultContract>(logger,  
                            new ValidatedUpdateCategoryNameRequest(
                                new UpdateCategoryNameUseCase(bus)))).Ask(body);
        }
        
        [HttpPatch]
        [Route("UpdateCategoryParentCategoryId")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateCategoryParentCategoryIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateCategoryParentCategoryIdErrorHttpResponse))] 
        public async Task<IActionResult> UpdateCategoryParentCategoryId(
            [FromBody] UpdateCategoryParentCategoryHttpBody body)
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<IUpdateCategoryParentCategoryIdRequestContract>) _services.GetService(typeof(ILogger<IUpdateCategoryParentCategoryIdRequestContract>));

            return await new CanonicalizedUpdateCategoryParentCategoryIdRequest(
                        new LoggedPipeNode<IUpdateCategoryParentCategoryIdRequestContract, IUpdateCategoryParentCategoryIdResultContract>(logger,  
                            new ValidatedUpdateCategoryParentCategoryIdRequest(
                                new UpdateCategoryParentCategoryIdUseCase(bus)))).Ask(body);
        }
    }
}