using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemHiddenComment
{
    public class LoggedUpdateStorageItemHiddenCommentRequest :
        PipeNodeBase<
            IUpdateStorageItemHiddenCommentRequestContract, 
            IUpdateStorageItemHiddenCommentResultContract>
    {
        private readonly ILogger<LoggedUpdateStorageItemHiddenCommentRequest> _logger;

        public LoggedUpdateStorageItemHiddenCommentRequest(
            ILogger<LoggedUpdateStorageItemHiddenCommentRequest> logger,
            IPipeNode<IUpdateStorageItemHiddenCommentRequestContract,
                IUpdateStorageItemHiddenCommentResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateStorageItemHiddenCommentResultContract> Ask(
            IUpdateStorageItemHiddenCommentRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"UpdateStorageItemHiddenCommentLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"UpdateStorageItemHiddenCommentLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}