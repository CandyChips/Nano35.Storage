using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllArticlesCategories
{
    public class GetAllArticlesCategoriesLogger :
        IPipelineNode<IGetAllArticlesCategoriesRequestContract, IGetAllArticlesCategoriesResultContract>
    {
        private readonly ILogger<GetAllArticlesCategoriesLogger> _logger;
        private readonly IPipelineNode<IGetAllArticlesCategoriesRequestContract, IGetAllArticlesCategoriesResultContract> _nextNode;

        public GetAllArticlesCategoriesLogger(
            ILogger<GetAllArticlesCategoriesLogger> logger,
            IPipelineNode<IGetAllArticlesCategoriesRequestContract, IGetAllArticlesCategoriesResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllArticlesCategoriesResultContract> Ask(
            IGetAllArticlesCategoriesRequestContract input)
        {
            _logger.LogInformation($"GetAllArticlesCategoriesLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetAllArticlesCategoriesLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}