using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllStorageItemsOnUnit
{
    public class LoggedGetAllStorageItemsOnUnitRequest :
        IPipelineNode<
            IGetAllStorageItemsOnUnitContract, 
            IGetAllStorageItemsOnUnitResultContract>
    {
        private readonly ILogger<LoggedGetAllStorageItemsOnUnitRequest> _logger;
        private readonly IPipelineNode<
            IGetAllStorageItemsOnUnitContract,
            IGetAllStorageItemsOnUnitResultContract> _nextNode;

        public LoggedGetAllStorageItemsOnUnitRequest(
            ILogger<LoggedGetAllStorageItemsOnUnitRequest> logger,
            IPipelineNode<
                IGetAllStorageItemsOnUnitContract,
                IGetAllStorageItemsOnUnitResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllStorageItemsOnUnitResultContract> Ask(
            IGetAllStorageItemsOnUnitContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllStorageItemsOnUnitLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllStorageItemsOnUnitLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}