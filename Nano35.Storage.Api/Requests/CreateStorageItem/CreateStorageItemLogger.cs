using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateStorageItem
{
    public class CreateStorageItemLogger :
        IPipelineNode<ICreateStorageItemRequestContract, ICreateStorageItemResultContract>
    {
        private readonly ILogger<CreateStorageItemLogger> _logger;
        private readonly IPipelineNode<ICreateStorageItemRequestContract, ICreateStorageItemResultContract> _nextNode;

        public CreateStorageItemLogger(
            ILogger<CreateStorageItemLogger> logger,
            IPipelineNode<ICreateStorageItemRequestContract, ICreateStorageItemResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreateStorageItemResultContract> Ask(
            ICreateStorageItemRequestContract input)
        {
            _logger.LogInformation($"CreateStorageItemLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"CreateStorageItemLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}