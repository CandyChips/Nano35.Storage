using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.CreateArticle
{
    public class LoggedCreateArticleRequest :
        IPipelineNode<
            ICreateArticleRequestContract,
            ICreateArticleResultContract>
    {
        private readonly ILogger<LoggedCreateArticleRequest> _logger;
        private readonly IPipelineNode<
            ICreateArticleRequestContract, 
            ICreateArticleResultContract> _nextNode;

        public LoggedCreateArticleRequest(
            ILogger<LoggedCreateArticleRequest> logger,
            IPipelineNode<
                ICreateArticleRequestContract,
                ICreateArticleResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreateArticleResultContract> Ask(
            ICreateArticleRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateArticleLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"CreateArticleLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}