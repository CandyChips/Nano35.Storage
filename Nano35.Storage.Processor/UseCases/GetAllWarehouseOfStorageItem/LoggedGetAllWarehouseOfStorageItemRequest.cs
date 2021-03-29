using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllWarehouseOfStorageItem
{
    public class LoggedGetAllWarehouseOfStorageItemRequest :
        IPipelineNode<
            IGetAllWarehouseOfStorageItemRequestContract, 
            IGetAllWarehouseOfStorageItemResultContract>
    {
        private readonly ILogger<LoggedGetAllWarehouseOfStorageItemRequest> _logger;
        private readonly IPipelineNode<
            IGetAllWarehouseOfStorageItemRequestContract,
            IGetAllWarehouseOfStorageItemResultContract> _nextNode;

        public LoggedGetAllWarehouseOfStorageItemRequest(
            ILogger<LoggedGetAllWarehouseOfStorageItemRequest> logger,
            IPipelineNode<
                IGetAllWarehouseOfStorageItemRequestContract,
                IGetAllWarehouseOfStorageItemResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllWarehouseOfStorageItemResultContract> Ask(
            IGetAllWarehouseOfStorageItemRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllWarehouseOfStorageItemLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllWarehouseOfStorageItemLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}