using System;
using System.Threading.Tasks;
using FluentValidation;
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

        [HttpPost]
        [Route("Category")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateCategorySuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateCategoryErrorHttpResponse))] 
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryHttpBody body) =>
            await new ConvertedCreateCategoryOnHttpContext(
                    new LoggedPipeNode<ICreateCategoryRequestContract, ICreateCategoryResultContract>(
                        _services.GetService(typeof(ILogger<ICreateCategoryRequestContract>)) as ILogger<ICreateCategoryRequestContract>,
                        new CreateCategoryUseCase(
                            _services.GetService(typeof(IBus)) as IBus)))
                .Ask(body);
        
        [HttpPatch]
        [Route("Name")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateCategoryNameSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateCategoryNameErrorHttpResponse))] 
        public async Task<IActionResult> UpdateCategoryName([FromBody] UpdateCategoryNameHttpBody body) =>
            await new ConvertedUpdateCategoryNameOnHttpContext(
                    new LoggedPipeNode<IUpdateCategoryNameRequestContract, IUpdateCategoryNameResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateCategoryNameRequestContract>)) as ILogger<IUpdateCategoryNameRequestContract>,
                        new UpdateCategoryNameUseCase(
                            _services.GetService(typeof(IBus)) as IBus)))
                .Ask(body);
        
        [HttpPatch]
        [Route("ParentCategoryId")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateCategoryParentCategoryIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateCategoryParentCategoryIdErrorHttpResponse))] 
        public async Task<IActionResult> UpdateCategoryParentCategoryId([FromBody] UpdateCategoryParentCategoryHttpBody body) =>
            await new ConvertedUpdateCategoryParentCategoryIdOnHttpContext(
                    new LoggedPipeNode<IUpdateCategoryParentCategoryIdRequestContract, IUpdateCategoryParentCategoryIdResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateCategoryParentCategoryIdRequestContract>)) as ILogger<IUpdateCategoryParentCategoryIdRequestContract>,
                        new UpdateCategoryParentCategoryIdUseCase(
                            _services.GetService(typeof(IBus)) as IBus)))
                .Ask(body);
    }
}