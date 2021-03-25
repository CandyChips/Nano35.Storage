using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemComment
{
    public class LoggedUpdateStorageItemCommentRequest :
        PipeNodeBase<IUpdateStorageItemCommentRequestContract, IUpdateStorageItemCommentResultContract>
    {
        private readonly ILogger<LoggedUpdateStorageItemCommentRequest> _logger;

        public LoggedUpdateStorageItemCommentRequest(
            ILogger<LoggedUpdateStorageItemCommentRequest> logger,
            IPipeNode<IUpdateStorageItemCommentRequestContract, IUpdateStorageItemCommentResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateStorageItemCommentResultContract> Ask(
            IUpdateStorageItemCommentRequestContract input)
        {
            _logger.LogInformation($"UpdateStorageItemCommentLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"UpdateStorageItemCommentLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}