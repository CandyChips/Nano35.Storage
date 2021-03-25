using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemPurchasePrice
{
    public class LoggedUpdateStorageItemPurchasePriceRequest :
        PipeNodeBase<IUpdateStorageItemPurchasePriceRequestContract, IUpdateStorageItemPurchasePriceResultContract>
    {
        private readonly ILogger<LoggedUpdateStorageItemPurchasePriceRequest> _logger;

        public LoggedUpdateStorageItemPurchasePriceRequest(
            ILogger<LoggedUpdateStorageItemPurchasePriceRequest> logger,
            IPipeNode<IUpdateStorageItemPurchasePriceRequestContract, IUpdateStorageItemPurchasePriceResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateStorageItemPurchasePriceResultContract> Ask(
            IUpdateStorageItemPurchasePriceRequestContract input)
        {
            _logger.LogInformation($"UpdateStorageItemPurchasePriceLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"UpdateStorageItemPurchasePriceLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}