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
using Nano35.Storage.Api.Requests.GetAllStorageItemConditions;

namespace Nano35.Storage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageItemConditionsController :
        ControllerBase
    {
        private readonly IServiceProvider _services;

        public StorageItemConditionsController(
            IServiceProvider services)
        {
            _services = services;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllStorageItemConditionsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllStorageItemConditionsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllStorageItemConditions() 
        {
            var result =
                await new LoggedUseCasePipeNode<IGetAllStorageItemConditionsRequestContract, IGetAllStorageItemConditionsResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllStorageItemConditionsRequestContract>)) as ILogger<IGetAllStorageItemConditionsRequestContract>,
                        new GetAllStorageItemConditionsUseCase(
                            _services.GetService((typeof(IBus))) as IBus))
                    .Ask(new GetAllStorageItemConditionsRequestContract(){});
            
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
    }

}