using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllArticleCategories
{
    public class LoggedGetAllArticlesCategoriesRequest :
        PipeNodeBase<IGetAllArticlesCategoriesRequestContract, IGetAllArticlesCategoriesResultContract>
    {
        private readonly ILogger<LoggedGetAllArticlesCategoriesRequest> _logger;

        public LoggedGetAllArticlesCategoriesRequest(
            ILogger<LoggedGetAllArticlesCategoriesRequest> logger,
            IPipeNode<IGetAllArticlesCategoriesRequestContract, IGetAllArticlesCategoriesResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllArticlesCategoriesResultContract> Ask(
            IGetAllArticlesCategoriesRequestContract input)
        {
            _logger.LogInformation($"GetAllArticlesCategoriesLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"GetAllArticlesCategoriesLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}