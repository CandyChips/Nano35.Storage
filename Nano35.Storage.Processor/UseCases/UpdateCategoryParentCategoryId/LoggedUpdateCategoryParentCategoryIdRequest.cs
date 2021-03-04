using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.UpdateCategoryParentCategoryId
{
    public class LoggedUpdateCategoryParentCategoryIdRequest :
        IPipelineNode<
            IUpdateCategoryParentCategoryIdRequestContract, 
            IUpdateCategoryParentCategoryIdResultContract>
    {
        private readonly ILogger<LoggedUpdateCategoryParentCategoryIdRequest> _logger;
        private readonly IPipelineNode<
            IUpdateCategoryParentCategoryIdRequestContract,
            IUpdateCategoryParentCategoryIdResultContract> _nextNode;

        public LoggedUpdateCategoryParentCategoryIdRequest(
            ILogger<LoggedUpdateCategoryParentCategoryIdRequest> logger,
            IPipelineNode<
                IUpdateCategoryParentCategoryIdRequestContract, 
                IUpdateCategoryParentCategoryIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateCategoryParentCategoryIdResultContract> Ask(
            IUpdateCategoryParentCategoryIdRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"UpdateCategoryParentCategoryIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"UpdateCategoryParentCategoryIdLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}