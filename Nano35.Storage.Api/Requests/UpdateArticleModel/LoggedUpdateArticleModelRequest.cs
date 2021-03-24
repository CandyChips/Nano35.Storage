using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateArticleModel
{
    public class LoggedUpdateArticleModelRequest :
        IPipelineNode<IUpdateArticleModelRequestContract, IUpdateArticleModelResultContract>
    {
        private readonly ILogger<LoggedUpdateArticleModelRequest> _logger;
        private readonly IPipelineNode<
            IUpdateArticleModelRequestContract, 
            IUpdateArticleModelResultContract> _nextNode;

        public LoggedUpdateArticleModelRequest(
            ILogger<LoggedUpdateArticleModelRequest> logger,
            IPipelineNode<
                IUpdateArticleModelRequestContract, 
                IUpdateArticleModelResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateArticleModelResultContract> Ask(
            IUpdateArticleModelRequestContract input)
        {
            _logger.LogInformation($"UpdateArticleModelLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdateArticleModelLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}