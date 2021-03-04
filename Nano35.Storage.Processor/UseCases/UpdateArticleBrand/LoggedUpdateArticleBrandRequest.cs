using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.UpdateArticleBrand
{
    public class LoggedUpdateArticleBrandRequest :
        IPipelineNode<
            IUpdateArticleBrandRequestContract, 
            IUpdateArticleBrandResultContract>
    {
        private readonly ILogger<LoggedUpdateArticleBrandRequest> _logger;
        private readonly IPipelineNode<
            IUpdateArticleBrandRequestContract,
            IUpdateArticleBrandResultContract> _nextNode;

        public LoggedUpdateArticleBrandRequest(
            ILogger<LoggedUpdateArticleBrandRequest> logger,
            IPipelineNode<
                IUpdateArticleBrandRequestContract, 
                IUpdateArticleBrandResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateArticleBrandResultContract> Ask(
            IUpdateArticleBrandRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"UpdateArticleBrandLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"UpdateArticleBrandLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}