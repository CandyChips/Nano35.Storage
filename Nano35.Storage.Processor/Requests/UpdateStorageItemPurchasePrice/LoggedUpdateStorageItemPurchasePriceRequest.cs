using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.UpdateStorageItemPurchasePrice
{
    public class LoggedUpdateStorageItemPurchasePriceRequest :
        IPipelineNode<
            IUpdateStorageItemPurchasePriceRequestContract, 
            IUpdateStorageItemPurchasePriceResultContract>
    {
        private readonly ILogger<LoggedUpdateStorageItemPurchasePriceRequest> _logger;
        private readonly IPipelineNode<
            IUpdateStorageItemPurchasePriceRequestContract,
            IUpdateStorageItemPurchasePriceResultContract> _nextNode;

        public LoggedUpdateStorageItemPurchasePriceRequest(
            ILogger<LoggedUpdateStorageItemPurchasePriceRequest> logger,
            IPipelineNode<
                IUpdateStorageItemPurchasePriceRequestContract, 
                IUpdateStorageItemPurchasePriceResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateStorageItemPurchasePriceResultContract> Ask(
            IUpdateStorageItemPurchasePriceRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"UpdateStorageItemPurchasePriceLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"UpdateStorageItemPurchasePriceLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}