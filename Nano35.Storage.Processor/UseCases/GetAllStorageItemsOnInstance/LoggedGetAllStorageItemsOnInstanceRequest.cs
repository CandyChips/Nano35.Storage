using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllStorageItemsOnInstance
{
    public class LoggedGetAllStorageItemsOnInstanceRequest :
        IPipelineNode<
            IGetAllStorageItemsOnInstanceContract, 
            IGetAllStorageItemsOnInstanceResultContract>
    {
        private readonly ILogger<LoggedGetAllStorageItemsOnInstanceRequest> _logger;
        private readonly IPipelineNode<
            IGetAllStorageItemsOnInstanceContract,
            IGetAllStorageItemsOnInstanceResultContract> _nextNode;

        public LoggedGetAllStorageItemsOnInstanceRequest(
            ILogger<LoggedGetAllStorageItemsOnInstanceRequest> logger,
            IPipelineNode<
                IGetAllStorageItemsOnInstanceContract,
                IGetAllStorageItemsOnInstanceResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllStorageItemsOnInstanceResultContract> Ask(
            IGetAllStorageItemsOnInstanceContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllStorageItemsOnInstanceLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllStorageItemsOnInstanceLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}