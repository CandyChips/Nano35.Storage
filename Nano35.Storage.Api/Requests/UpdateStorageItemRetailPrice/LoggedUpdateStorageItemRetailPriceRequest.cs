using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemRetailPrice
{
    public class LoggedUpdateStorageItemRetailPriceRequest :
        IPipelineNode<
            IUpdateStorageItemRetailPriceRequestContract,
            IUpdateStorageItemRetailPriceResultContract>
    {
        private readonly ILogger<LoggedUpdateStorageItemRetailPriceRequest> _logger;
        private readonly IPipelineNode<
            IUpdateStorageItemRetailPriceRequestContract, 
            IUpdateStorageItemRetailPriceResultContract> _nextNode;

        public LoggedUpdateStorageItemRetailPriceRequest(
            ILogger<LoggedUpdateStorageItemRetailPriceRequest> logger,
            IPipelineNode<
                IUpdateStorageItemRetailPriceRequestContract, 
                IUpdateStorageItemRetailPriceResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateStorageItemRetailPriceResultContract> Ask(
            IUpdateStorageItemRetailPriceRequestContract input)
        {
            _logger.LogInformation($"UpdateStorageItemRetailPriceLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdateStorageItemRetailPriceLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}