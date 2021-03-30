using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllPlacesOfStorageItemOnInstance
{
    public class LoggedGetAllPlacesOfStorageItemOnInstanceRequest :
        IPipelineNode<
            IGetAllPlacesOfStorageItemOnInstanceContract, 
            IGetAllPlacesOfStorageItemOnInstanceResultContract>
    {
        private readonly ILogger<LoggedGetAllPlacesOfStorageItemOnInstanceRequest> _logger;
        private readonly IPipelineNode<
            IGetAllPlacesOfStorageItemOnInstanceContract,
            IGetAllPlacesOfStorageItemOnInstanceResultContract> _nextNode;

        public LoggedGetAllPlacesOfStorageItemOnInstanceRequest(
            ILogger<LoggedGetAllPlacesOfStorageItemOnInstanceRequest> logger,
            IPipelineNode<
                IGetAllPlacesOfStorageItemOnInstanceContract,
                IGetAllPlacesOfStorageItemOnInstanceResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllPlacesOfStorageItemOnInstanceResultContract> Ask(
            IGetAllPlacesOfStorageItemOnInstanceContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllPlacesOfStorageItemOnInstanceLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllPlacesOfStorageItemOnInstanceLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}