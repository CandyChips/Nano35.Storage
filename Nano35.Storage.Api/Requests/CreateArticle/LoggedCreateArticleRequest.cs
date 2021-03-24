using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateArticle
{
    public class LoggedCreateArticleRequest :
        PipeNodeBase<ICreateArticleRequestContract, ICreateArticleResultContract>
    {
        private readonly ILogger<LoggedCreateArticleRequest> _logger;

        public LoggedCreateArticleRequest(
            ILogger<LoggedCreateArticleRequest> logger,
            IPipeNode<ICreateArticleRequestContract, ICreateArticleResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<ICreateArticleResultContract> Ask(
            ICreateArticleRequestContract input)
        {
            _logger.LogInformation($"CreateArticleLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"CreateArticleLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}