using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllArticles
{
    public class LoggedGetAllArticlesRequest :
        PipeNodeBase<IGetAllArticlesRequestContract, IGetAllArticlesResultContract>
    {
        private readonly ILogger<LoggedGetAllArticlesRequest> _logger;

        public LoggedGetAllArticlesRequest(
            ILogger<LoggedGetAllArticlesRequest> logger,
            IPipeNode<IGetAllArticlesRequestContract, IGetAllArticlesResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllArticlesResultContract> Ask(
            IGetAllArticlesRequestContract input)
        {
            _logger.LogInformation($"GetAllArticlesLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"GetAllArticlesLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}