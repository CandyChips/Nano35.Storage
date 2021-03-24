using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllPlacesOnStorage
{
    public class LoggedGetAllPlacesOnStorageRequest :
        PipeNodeBase<IGetAllPlacesOnStorageContract, IGetAllPlacesOnStorageResultContract>
    {
        private readonly ILogger<LoggedGetAllPlacesOnStorageRequest> _logger;

        public LoggedGetAllPlacesOnStorageRequest(
            ILogger<LoggedGetAllPlacesOnStorageRequest> logger,
            IPipeNode<IGetAllPlacesOnStorageContract, IGetAllPlacesOnStorageResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllPlacesOnStorageResultContract> Ask(
            IGetAllPlacesOnStorageContract input)
        {
            _logger.LogInformation($"GetAllPlacesOnStorageLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"GetAllPlacesOnStorageLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}