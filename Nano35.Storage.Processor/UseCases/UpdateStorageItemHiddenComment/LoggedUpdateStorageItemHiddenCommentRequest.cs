using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemHiddenComment
{
    public class LoggedUpdateStorageItemHiddenCommentRequest :
        IPipelineNode<
            IUpdateStorageItemHiddenCommentRequestContract, 
            IUpdateStorageItemHiddenCommentResultContract>
    {
        private readonly ILogger<LoggedUpdateStorageItemHiddenCommentRequest> _logger;
        private readonly IPipelineNode<
            IUpdateStorageItemHiddenCommentRequestContract,
            IUpdateStorageItemHiddenCommentResultContract> _nextNode;

        public LoggedUpdateStorageItemHiddenCommentRequest(
            ILogger<LoggedUpdateStorageItemHiddenCommentRequest> logger,
            IPipelineNode<
                IUpdateStorageItemHiddenCommentRequestContract, 
                IUpdateStorageItemHiddenCommentResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateStorageItemHiddenCommentResultContract> Ask(
            IUpdateStorageItemHiddenCommentRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"UpdateStorageItemHiddenCommentLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"UpdateStorageItemHiddenCommentLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}