using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetArticleById
{
    public class LoggedGetArticleByIdRequest :
        PipeNodeBase<IGetArticleByIdRequestContract, IGetArticleByIdResultContract>
    {
        private readonly ILogger<LoggedGetArticleByIdRequest> _logger;

        public LoggedGetArticleByIdRequest(
            ILogger<LoggedGetArticleByIdRequest> logger,
            IPipeNode<IGetArticleByIdRequestContract, IGetArticleByIdResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetArticleByIdResultContract> Ask(
            IGetArticleByIdRequestContract input)
        {
            _logger.LogInformation($"GetArticleByIdLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"GetArticleByIdLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}