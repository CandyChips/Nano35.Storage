using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateStorageItem
{
    public class LoggedCreateStorageItemRequest :
        PipeNodeBase<ICreateStorageItemRequestContract, ICreateStorageItemResultContract>
    {
        private readonly ILogger<LoggedCreateStorageItemRequest> _logger;

        public LoggedCreateStorageItemRequest(
            ILogger<LoggedCreateStorageItemRequest> logger,
            IPipeNode<ICreateStorageItemRequestContract, ICreateStorageItemResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<ICreateStorageItemResultContract> Ask(
            ICreateStorageItemRequestContract input)
        {
            _logger.LogInformation($"CreateStorageItemLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"CreateStorageItemLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}