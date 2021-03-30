using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllPlacesOfStorageItemOnUnit
{
    public class LoggedGetAllPlacesOfStorageItemOnUnitRequest :
        IPipelineNode<
            IGetAllPlacesOfStorageItemOnUnitRequestContract, 
            IGetAllPlacesOfStorageItemOnUnitResultContract>
    {
        private readonly ILogger<LoggedGetAllPlacesOfStorageItemOnUnitRequest> _logger;
        private readonly IPipelineNode<
            IGetAllPlacesOfStorageItemOnUnitRequestContract,
            IGetAllPlacesOfStorageItemOnUnitResultContract> _nextNode;

        public LoggedGetAllPlacesOfStorageItemOnUnitRequest(
            ILogger<LoggedGetAllPlacesOfStorageItemOnUnitRequest> logger,
            IPipelineNode<
                IGetAllPlacesOfStorageItemOnUnitRequestContract,
                IGetAllPlacesOfStorageItemOnUnitResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllPlacesOfStorageItemOnUnitResultContract> Ask(
            IGetAllPlacesOfStorageItemOnUnitRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllPlacesOfStorageItemOnUnitLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllPlacesOfStorageItemOnUnitLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}