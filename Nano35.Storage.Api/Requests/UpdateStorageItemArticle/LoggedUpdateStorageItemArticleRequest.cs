using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemArticle
{
    public class LoggedUpdateStorageItemArticleRequest :
        IPipelineNode<
            IUpdateStorageItemArticleRequestContract,
            IUpdateStorageItemArticleResultContract>
    {
        private readonly ILogger<LoggedUpdateStorageItemArticleRequest> _logger;
        private readonly IPipelineNode<
            IUpdateStorageItemArticleRequestContract, 
            IUpdateStorageItemArticleResultContract> _nextNode;

        public LoggedUpdateStorageItemArticleRequest(
            ILogger<LoggedUpdateStorageItemArticleRequest> logger,
            IPipelineNode<
                IUpdateStorageItemArticleRequestContract, 
                IUpdateStorageItemArticleResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateStorageItemArticleResultContract> Ask(
            IUpdateStorageItemArticleRequestContract input)
        {
            _logger.LogInformation($"UpdateStorageItemArticleLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdateStorageItemArticleLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}