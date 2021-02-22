using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateArticleCategory
{
    public class LoggedUpdateArticleCategoryRequest :
        IPipelineNode<
            IUpdateArticleCategoryRequestContract,
            IUpdateArticleCategoryResultContract>
    {
        private readonly ILogger<LoggedUpdateArticleCategoryRequest> _logger;
        private readonly IPipelineNode<
            IUpdateArticleCategoryRequestContract, 
            IUpdateArticleCategoryResultContract> _nextNode;

        public LoggedUpdateArticleCategoryRequest(
            ILogger<LoggedUpdateArticleCategoryRequest> logger,
            IPipelineNode<
                IUpdateArticleCategoryRequestContract, 
                IUpdateArticleCategoryResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateArticleCategoryResultContract> Ask(
            IUpdateArticleCategoryRequestContract input)
        {
            _logger.LogInformation($"UpdateArticleCategoryLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdateArticleCategoryLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}