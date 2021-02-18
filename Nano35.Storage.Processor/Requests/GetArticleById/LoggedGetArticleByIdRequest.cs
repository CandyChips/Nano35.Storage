using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.GetArticleById
{
    public class LoggedGetArticleByIdRequest :
        IPipelineNode<
            IGetArticleByIdRequestContract, 
            IGetArticleByIdResultContract>
    {
        private readonly ILogger<LoggedGetArticleByIdRequest> _logger;
        private readonly IPipelineNode<
            IGetArticleByIdRequestContract,
            IGetArticleByIdResultContract> _nextNode;

        public LoggedGetArticleByIdRequest(
            ILogger<LoggedGetArticleByIdRequest> logger,
            IPipelineNode<
                IGetArticleByIdRequestContract,
                IGetArticleByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetArticleByIdResultContract> Ask(
            IGetArticleByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetArticleByIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetArticleByIdLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}