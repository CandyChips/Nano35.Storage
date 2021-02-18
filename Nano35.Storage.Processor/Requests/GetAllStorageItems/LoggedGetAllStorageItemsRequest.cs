using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.GetAllStorageItems
{
    public class LoggedGetAllStorageItemsRequest :
        IPipelineNode<
            IGetAllStorageItemsRequestContract, 
            IGetAllStorageItemsResultContract>
    {
        private readonly ILogger<LoggedGetAllStorageItemsRequest> _logger;
        private readonly IPipelineNode<
            IGetAllStorageItemsRequestContract,
            IGetAllStorageItemsResultContract> _nextNode;

        public LoggedGetAllStorageItemsRequest(
            ILogger<LoggedGetAllStorageItemsRequest> logger,
            IPipelineNode<
                IGetAllStorageItemsRequestContract,
                IGetAllStorageItemsResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllStorageItemsResultContract> Ask(
            IGetAllStorageItemsRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllStorageItemsLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllStorageItemsLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}