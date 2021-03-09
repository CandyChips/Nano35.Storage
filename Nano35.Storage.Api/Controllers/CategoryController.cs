﻿using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.instance;
using Nano35.HttpContext.storage;
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
        
        public CategoryController(
            IServiceProvider services)
        {
            _services = services;
        }
    
        [HttpGet]
        [Route("GetAllArticleCategories")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllArticleCategoriesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllArticleCategoriesErrorHttpResponse))] 
        public async Task<IActionResult> GetAllArticleCategories(
            [FromQuery] GetAllArticlesCategoriesHttpQuery query)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllArticlesCategoriesRequest>)_services.GetService(typeof(ILogger<LoggedGetAllArticlesCategoriesRequest>));

            var request = new GetAllArticlesCategoriesRequestContract()
            {
                InstanceId = query.InstanceId,
                ParentId = query.ParentId
            };
            
            var result = 
                await new LoggedGetAllArticlesCategoriesRequest(logger,
                    new ValidatedGetAllArticlesCategoriesRequest(
                        new GetAllArticlesCategoriesRequest(bus)))
                    .Ask(request);
            
            return result switch
            {
                IGetAllArticlesCategoriesSuccessResultContract success => Ok(success),
                IGetAllArticlesCategoriesErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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
            var logger = (ILogger<LoggedCreateCategoryRequest>)_services.GetService(typeof(ILogger<LoggedCreateCategoryRequest>));

            var request = new CreateCategoryRequestContract()
            {
                InstanceId = body.InstanceId,
                NewId = body.NewId,
                Name = body.Name,
                ParentCategoryId = body.ParentCategoryId
            };
            
            var result = 
                await new LoggedCreateCategoryRequest(logger,
                    new ValidatedCreateCategoryRequest(
                        new CreateCategoryRequest(bus)))
                    .Ask(request);
            
            return result switch
            {
                ICreateCategorySuccessResultContract => Ok(),
                ICreateCategoryErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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
            var logger = (ILogger<LoggedUpdateCategoryNameRequest>) _services.GetService(typeof(ILogger<LoggedUpdateCategoryNameRequest>));

            var request = new UpdateCategoryNameRequestContract()
            {
                Id = body.Id,
                Name = body.Name
            };
            
            var result =
                await new LoggedUpdateCategoryNameRequest(logger,  
                    new ValidatedUpdateCategoryNameRequest(
                        new UpdateCategoryNameRequest(bus)))
                    .Ask(request);
            
            return result switch
            {
                IUpdateCategoryNameSuccessResultContract => Ok(),
                IUpdateCategoryNameErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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
            var logger = (ILogger<LoggedUpdateCategoryParentCategoryIdRequest>) _services
                .GetService(typeof(ILogger<LoggedUpdateCategoryParentCategoryIdRequest>));

            var request = new UpdateCategoryParentCategoryIdRequestContract()
            {
                Id = body.Id,
                ParentCategoryId = body.ParentCategoryId
            };
            
            var result =
                await new LoggedUpdateCategoryParentCategoryIdRequest(logger,  
                    new ValidatedUpdateCategoryParentCategoryIdRequest(
                        new UpdateCategoryParentCategoryIdRequest(bus)
                    )
                ).Ask(request);
            
            return result switch
            {
                IUpdateCategoryParentCategoryIdSuccessResultContract => Ok(),
                IUpdateCategoryParentCategoryIdErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }
    }
}