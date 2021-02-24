using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Api.HttpContext;
using Nano35.Storage.Api.Requests.CreateCategory;
using Nano35.Storage.Api.Requests.UpdateCategoryName;
using Nano35.Storage.Api.Requests.UpdateCategoryParentCategoryId;

namespace Nano35.Storage.Api.Controllers
{
    /// ToDo Hey Maslyonok
    /// <summary>
    /// http://localhost:5104/articles/[action]
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CategoryController :
        ControllerBase
    {
        public class UpdateCategoryNameHttpContext : IUpdateCategoryNameRequestContract
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
        
        public class UpdateCategoryParentCategoryIdHttpContext : IUpdateCategoryParentCategoryIdRequestContract
        {
            public Guid Id { get; set; }
            public Guid ParentCategoryId { get; set; }
        }
        
        
        private readonly IServiceProvider _services;
        
        /// ToDo Hey Maslyonok
        /// <summary>
        /// Controller provide IServiceProvider from asp net core DI
        /// for registration services to pipe nodes
        /// </summary>
        public CategoryController(
            IServiceProvider services)
        {
            _services = services;
        }
        
        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IActionResult> CreateCategory(
            [FromBody] CreateCategoryHttpContext query)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedCreateCategoryRequest>)_services.GetService(typeof(ILogger<LoggedCreateCategoryRequest>));
            
            // Send request to pipeline
            var result = 
                await new LoggedCreateCategoryRequest(logger,
                    new ValidatedCreateCategoryRequest(
                        new CreateCategoryRequest(bus)
                    )).Ask(query);
            
            // Check response of get all instances request
            // You can check result by result contracts
            return result switch
            {
                ICreateCategorySuccessResultContract => Ok(),
                ICreateCategoryErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
        
        [HttpPatch]
        [Route("UpdateCategoryName")]
        public async Task<IActionResult> UpdateCategoryName(
            [FromBody] UpdateCategoryNameHttpContext body)
        {
            
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateCategoryNameRequest>) _services.GetService(typeof(ILogger<LoggedUpdateCategoryNameRequest>));

            var result =
                await new LoggedUpdateCategoryNameRequest(logger,  
                    new ValidatedUpdateCategoryNameRequest(
                        new UpdateCategoryNameRequest(bus)
                    )
                ).Ask(body);
            
            return result switch
            {
                IUpdateCategoryNameSuccessResultContract => Ok(),
                IUpdateCategoryNameErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
        
        [HttpPatch]
        [Route("UpdateCategoryParentCategoryId")]
        public async Task<IActionResult> UpdateCategoryParentCategoryId(
            [FromBody] UpdateCategoryParentCategoryIdHttpContext body)
        {
            
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateCategoryParentCategoryIdRequest>) _services
                .GetService(typeof(ILogger<LoggedUpdateCategoryParentCategoryIdRequest>));

            var result =
                await new LoggedUpdateCategoryParentCategoryIdRequest(logger,  
                    new ValidatedUpdateCategoryParentCategoryIdRequest(
                        new UpdateCategoryParentCategoryIdRequest(bus)
                    )
                ).Ask(body);
            
            return result switch
            {
                IUpdateCategoryParentCategoryIdSuccessResultContract => Ok(),
                IUpdateCategoryParentCategoryIdErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    }
}