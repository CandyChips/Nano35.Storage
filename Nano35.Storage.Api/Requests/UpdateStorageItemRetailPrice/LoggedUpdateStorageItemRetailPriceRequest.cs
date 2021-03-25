using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemRetailPrice
{
    public class LoggedUpdateStorageItemRetailPriceRequest :
        PipeNodeBase<IUpdateStorageItemRetailPriceRequestContract, IUpdateStorageItemRetailPriceResultContract>
    {
        private readonly ILogger<LoggedUpdateStorageItemRetailPriceRequest> _logger;

        public LoggedUpdateStorageItemRetailPriceRequest(
            ILogger<LoggedUpdateStorageItemRetailPriceRequest> logger,
            IPipeNode<IUpdateStorageItemRetailPriceRequestContract, IUpdateStorageItemRetailPriceResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateStorageItemRetailPriceResultContract> Ask(
            IUpdateStorageItemRetailPriceRequestContract input)
        {
            _logger.LogInformation($"UpdateStorageItemRetailPriceLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"UpdateStorageItemRetailPriceLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}