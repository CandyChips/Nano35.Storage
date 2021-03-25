using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateArticleModel
{
    public class LoggedUpdateArticleModelRequest :
        PipeNodeBase<IUpdateArticleModelRequestContract, IUpdateArticleModelResultContract>
    {
        private readonly ILogger<LoggedUpdateArticleModelRequest> _logger;

        public LoggedUpdateArticleModelRequest(
            ILogger<LoggedUpdateArticleModelRequest> logger,
            IPipeNode<IUpdateArticleModelRequestContract, IUpdateArticleModelResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateArticleModelResultContract> Ask(
            IUpdateArticleModelRequestContract input)
        {
            _logger.LogInformation($"UpdateArticleModelLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"UpdateArticleModelLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}