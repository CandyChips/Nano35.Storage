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
using Nano35.Storage.Api.Requests.CreateComing;
using Nano35.Storage.Api.Requests.GetAllComingDetails;
using Nano35.Storage.Api.Requests.GetAllComings;
using CreateComingDetailViewModel = Nano35.Contracts.Storage.Models.CreateComingDetailViewModel;

namespace Nano35.Storage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComingsController:
        ControllerBase
    {
        private readonly IServiceProvider _services;

        public ComingsController(
            IServiceProvider services)
        {
            _services = services;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateComingSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateComingErrorHttpResponse))] 
        public async Task<IActionResult> CreateComing(
            [FromBody] CreateComingHttpBody body) {
            var result =
                await new LoggedUseCasePipeNode<ICreateComingRequestContract, ICreateComingResultContract>(
                        _services.GetService(typeof(ILogger<ICreateComingRequestContract>)) as ILogger<ICreateComingRequestContract>,
                        new CreateComingUseCase(
                            _services.GetService((typeof(IBus))) as IBus))
                    .Ask(new CreateComingRequestContract()
                    {
                        NewId = body.NewId,
                        InstanceId = body.InstanceId,
                        UnitId = body.UnitId,
                        Number = body.Number,
                        Comment = body.Comment,
                        ClientId = body.ClientId,
                        Details = body
                            .Details
                            .Select(g=> 
                                new CreateComingDetailViewModel()
                                {
                                    NewId = g.NewId,
                                    Count = g.Count,
                                    PlaceOnStorage = g.PlaceOnStorage,
                                    StorageItemId = g.StorageItemId,
                                    Price = g.Price
                                })
                            .ToList()
                    });
            
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
        
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllComingsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllComingsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllComings(
            [FromHeader] GetAllComingsHttpQuery query) 
        {
            var result =
                await new LoggedUseCasePipeNode<IGetAllComingsRequestContract, IGetAllComingsResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllComingsRequestContract>)) as ILogger<IGetAllComingsRequestContract>,
                        new GetAllComingsUseCase(
                            _services.GetService((typeof(IBus))) as IBus))
                    .Ask(new GetAllComingsRequestContract()
                    {
                        InstanceId = query.InstanceId,
                        UnitId = query.UnitId,
                        StorageItemId = query.StorageItemId
                    });
            
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
        
        [HttpGet]
        [Route("{id}/Details")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetComingDetailsByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetComingDetailsByIdErrorHttpResponse))] 
        public async Task<IActionResult> GetComingDetails(Guid id) 
        {
            var result =
                await new LoggedUseCasePipeNode<IGetAllComingDetailsRequestContract, IGetAllComingDetailsResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllComingDetailsRequestContract>)) as ILogger<IGetAllComingDetailsRequestContract>,
                        new GetAllComingDetailsUseCase(
                            _services.GetService((typeof(IBus))) as IBus))
                    .Ask(new GetAllComingDetailsRequestContract() {ComingId = id});
            
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
    }
}