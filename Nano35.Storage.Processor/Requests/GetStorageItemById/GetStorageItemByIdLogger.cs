using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.GetAllArticleBrands
{
    public class GetStorageItemByIdLogger :
        IPipelineNode<
            IGetStorageItemByIdRequestContract, 
            IGetStorageItemByIdResultContract>
    {
        private readonly ILogger<GetStorageItemByIdLogger> _logger;
        private readonly IPipelineNode<
            IGetStorageItemByIdRequestContract,
            IGetStorageItemByIdResultContract> _nextNode;

        public GetStorageItemByIdLogger(
            ILogger<GetStorageItemByIdLogger> logger,
            IPipelineNode<
                IGetStorageItemByIdRequestContract,
                IGetStorageItemByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetStorageItemByIdResultContract> Ask(
            IGetStorageItemByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetStorageItemByIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetStorageItemByIdLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}