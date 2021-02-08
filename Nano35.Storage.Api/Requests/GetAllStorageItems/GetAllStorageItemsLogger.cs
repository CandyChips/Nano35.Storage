using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllStorageItems
{
    public class GetAllStorageItemsLogger :
        IPipelineNode<IGetAllStorageItemsRequestContract, IGetAllStorageItemsResultContract>
    {
        private readonly ILogger<GetAllStorageItemsLogger> _logger;
        private readonly IPipelineNode<IGetAllStorageItemsRequestContract, IGetAllStorageItemsResultContract> _nextNode;

        public GetAllStorageItemsLogger(
            ILogger<GetAllStorageItemsLogger> logger,
            IPipelineNode<IGetAllStorageItemsRequestContract, IGetAllStorageItemsResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllStorageItemsResultContract> Ask(
            IGetAllStorageItemsRequestContract input)
        {
            _logger.LogInformation($"GetAllStorageItemsLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetAllStorageItemsLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}