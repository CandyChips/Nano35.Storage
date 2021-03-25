using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemHiddenComment
{
    public class LoggedUpdateStorageItemHiddenCommentRequest :
        PipeNodeBase<IUpdateStorageItemHiddenCommentRequestContract, IUpdateStorageItemHiddenCommentResultContract>
    {
        private readonly ILogger<LoggedUpdateStorageItemHiddenCommentRequest> _logger;

        public LoggedUpdateStorageItemHiddenCommentRequest(
            ILogger<LoggedUpdateStorageItemHiddenCommentRequest> logger,
            IPipeNode<IUpdateStorageItemHiddenCommentRequestContract, IUpdateStorageItemHiddenCommentResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateStorageItemHiddenCommentResultContract> Ask(
            IUpdateStorageItemHiddenCommentRequestContract input)
        {
            _logger.LogInformation($"UpdateStorageItemHiddenCommentLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"UpdateStorageItemHiddenCommentLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}