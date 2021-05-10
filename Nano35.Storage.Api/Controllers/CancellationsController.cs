using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.HttpContext.storage;
using Nano35.Storage.Api.Requests;
using Nano35.Storage.Api.Requests.CreateCancellation;
using Nano35.Storage.Api.Requests.GetAllArticleBrands;
using Nano35.Storage.Api.Requests.GetAllCancellationDetails;
using Nano35.Storage.Api.Requests.GetAllCancellations;
using CreateCancellationDetailViewModel = Nano35.Contracts.Storage.Models.CreateCancellationDetailViewModel;

namespace Nano35.Storage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CancellationsController :
        ControllerBase
    {
        private readonly IServiceProvider _services;

        public CancellationsController(
            IServiceProvider services)
        {
            _services = services;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateCancellationSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateCancellationErrorHttpResponse))] 
        public async Task<IActionResult> CreateCancellation([FromBody] CreateCancellationHttpBody body) 
        {
            var result =
                await new LoggedUseCasePipeNode<ICreateCancellationRequestContract, ICreateCancellationResultContract>(
                        _services.GetService(typeof(ILogger<ICreateCancellationRequestContract>)) as ILogger<ICreateCancellationRequestContract>, 
                        new CreateCancellationUseCase(_services.GetService(typeof(IBus)) as IBus))
                    .Ask(new CreateCancellationRequestContract()
                    {
                        NewId = body.NewId,
                        InstanceId = body.InstanceId,
                        UnitId = body.UnitId,
                        Comment = body.Comment,
                        Details = body
                            .Details
                            .Select(a=> 
                                new CreateCancellationDetailViewModel()
                                    {NewId = a.NewId,
                                     Count = a.Count,
                                     PlaceOnStorage = a.PlaceOnStorage,
                                     StorageItemId = a.StorageItemId})
                            .ToList()
                    });
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCancellations([FromQuery] GetAllCancellationsHttpQuery body) {
            var result =
                await new LoggedUseCasePipeNode<IGetAllCancellationsRequestContract, IGetAllCancellationsResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllCancellationsRequestContract>)) as ILogger<IGetAllCancellationsRequestContract>,
                        new GetAllCancellationsUseCase(
                            _services.GetService((typeof(IBus))) as IBus))
                    .Ask(new GetAllCancellationsRequestContract()
                    {
                        InstanceId = body.InstanceId,
                        UnitId = body.UnitId,
                        StorageItemId = body.StorageItemId
                    });
            
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
        
        [HttpGet]
        [Route("{id}/Details")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllSellDetailsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllSellDetailsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllCancellationDetails(Guid id)
        {
            var result =
                await new LoggedUseCasePipeNode<IGetAllCancellationDetailsRequestContract,
                        IGetAllCancellationDetailsResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllCancellationDetailsRequestContract>)) as
                            ILogger<IGetAllCancellationDetailsRequestContract>,
                        new GetAllCancellationDetailsUseCase(
                            _services.GetService((typeof(IBus))) as IBus))
                    .Ask(new GetAllCancellationDetailsRequestContract()
                    {
                        CancellationId = id
                    });
            
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
    }
}