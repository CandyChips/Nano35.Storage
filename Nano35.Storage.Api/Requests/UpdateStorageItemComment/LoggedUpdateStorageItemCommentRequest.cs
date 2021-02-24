using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemComment
{
    public class LoggedUpdateStorageItemCommentRequest :
        IPipelineNode<
            IUpdateStorageItemCommentRequestContract,
            IUpdateStorageItemCommentResultContract>
    {
        private readonly ILogger<LoggedUpdateStorageItemCommentRequest> _logger;
        private readonly IPipelineNode<
            IUpdateStorageItemCommentRequestContract, 
            IUpdateStorageItemCommentResultContract> _nextNode;

        public LoggedUpdateStorageItemCommentRequest(
            ILogger<LoggedUpdateStorageItemCommentRequest> logger,
            IPipelineNode<
                IUpdateStorageItemCommentRequestContract, 
                IUpdateStorageItemCommentResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateStorageItemCommentResultContract> Ask(
            IUpdateStorageItemCommentRequestContract input)
        {
            _logger.LogInformation($"UpdateStorageItemCommentLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdateStorageItemCommentLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}