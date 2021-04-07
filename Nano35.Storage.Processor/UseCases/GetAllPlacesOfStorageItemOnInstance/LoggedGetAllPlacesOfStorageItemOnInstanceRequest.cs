using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllPlacesOfStorageItemOnInstance
{
    public class LoggedGetAllPlacesOfStorageItemOnInstanceRequest :
        PipeNodeBase<
            IGetAllPlacesOfStorageItemOnInstanceContract, 
            IGetAllPlacesOfStorageItemOnInstanceResultContract>
    {
        private readonly ILogger<LoggedGetAllPlacesOfStorageItemOnInstanceRequest> _logger;

        public LoggedGetAllPlacesOfStorageItemOnInstanceRequest(
            ILogger<LoggedGetAllPlacesOfStorageItemOnInstanceRequest> logger,
            IPipeNode<IGetAllPlacesOfStorageItemOnInstanceContract,
                IGetAllPlacesOfStorageItemOnInstanceResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllPlacesOfStorageItemOnInstanceResultContract> Ask(
            IGetAllPlacesOfStorageItemOnInstanceContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllPlacesOfStorageItemOnInstanceLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"GetAllPlacesOfStorageItemOnInstanceLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}