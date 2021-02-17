using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetStorageItemById
{
    public class LoggedGetStorageItemByIdRequest :
        IPipelineNode<
            IGetStorageItemByIdRequestContract,
            IGetStorageItemByIdResultContract>
    {
        private readonly ILogger<LoggedGetStorageItemByIdRequest> _logger;
        private readonly IPipelineNode<
            IGetStorageItemByIdRequestContract, 
            IGetStorageItemByIdResultContract> _nextNode;

        public LoggedGetStorageItemByIdRequest(
            ILogger<LoggedGetStorageItemByIdRequest> logger,
            IPipelineNode<
                IGetStorageItemByIdRequestContract, 
                IGetStorageItemByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetStorageItemByIdResultContract> Ask(
            IGetStorageItemByIdRequestContract input)
        {
            _logger.LogInformation($"GetStorageItemByIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetStorageItemByIdLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}