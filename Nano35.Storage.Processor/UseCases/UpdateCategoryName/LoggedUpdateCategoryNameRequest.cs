using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.UpdateCategoryName
{
    public class LoggedUpdateCategoryNameRequest :
        IPipelineNode<
            IUpdateCategoryNameRequestContract, 
            IUpdateCategoryNameResultContract>
    {
        private readonly ILogger<LoggedUpdateCategoryNameRequest> _logger;
        private readonly IPipelineNode<
            IUpdateCategoryNameRequestContract,
            IUpdateCategoryNameResultContract> _nextNode;

        public LoggedUpdateCategoryNameRequest(
            ILogger<LoggedUpdateCategoryNameRequest> logger,
            IPipelineNode<
                IUpdateCategoryNameRequestContract, 
                IUpdateCategoryNameResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateCategoryNameResultContract> Ask(
            IUpdateCategoryNameRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"UpdateCategoryNameLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"UpdateCategoryNameLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}