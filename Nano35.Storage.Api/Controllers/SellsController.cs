using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.storage;
using Nano35.Storage.Api.Requests;
using Nano35.Storage.Api.Requests.CreateSelle;
using Nano35.Storage.Api.Requests.GetAllSelleDetails;
using Nano35.Storage.Api.Requests.GetAllSells;

namespace Nano35.Storage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SellsController:
        ControllerBase
    {
        private readonly IServiceProvider _services;

        public SellsController(
            IServiceProvider services)
        {
            _services = services;
        }

        [HttpPost]
        [Route("Selle")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateSelleSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateSelleErrorHttpResponse))] 
        public async Task<IActionResult> CreateSelle(
            [FromBody] CreateSelleHttpBody body) =>
            await new ConvertedCreateSelleOnHttpContext(
                new LoggedPipeNode<ICreateSelleRequestContract, ICreateSelleResultContract>(
                    _services.GetService(typeof(ILogger<ICreateSelleRequestContract>)) as ILogger<ICreateSelleRequestContract>,
                    new CreateSelleUseCase(_services.GetService(typeof(IBus)) as IBus))).Ask(body);

        [HttpGet]
        [Route("Sells")]
        public async Task<IActionResult> GetAllSells(
            [FromQuery] GetAllSellsHttpQuery body) =>
            await new ConvertedGetAllSellsOnHttpContext(
                    new LoggedPipeNode<IGetAllSellsRequestContract, IGetAllSellsResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllSellsRequestContract>)) as ILogger<IGetAllSellsRequestContract>,
                        new GetAllSellsUseCase(_services.GetService(typeof(IBus)) as IBus)))
                .Ask(body);

        
        [HttpGet]
        [Route("SellDetails")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllSellDetailsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllSellDetailsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllSellDetails(
            [FromQuery] GetAllSellDetailsHttpQuery query) =>
            await new ConvertedGetAllSelleDetailsOnHttpContext(
                    new LoggedPipeNode<IGetAllSelleDetailsRequestContract, IGetAllSelleDetailsResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllSelleDetailsRequestContract>)) as ILogger<IGetAllSelleDetailsRequestContract>,
                        new GetAllSelleDetailsUseCase(_services.GetService(typeof(IBus)) as IBus)))
                .Ask(query);
    }
}