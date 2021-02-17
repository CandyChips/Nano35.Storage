using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllArticleModels
{
    public class LoggedGetAllArticlesModelsRequest :
        IPipelineNode<
            IGetAllArticlesModelsRequestContract,
            IGetAllArticlesModelsResultContract>
    {
        private readonly ILogger<LoggedGetAllArticlesModelsRequest> _logger;
        private readonly IPipelineNode<
            IGetAllArticlesModelsRequestContract,
            IGetAllArticlesModelsResultContract> _nextNode;

        public LoggedGetAllArticlesModelsRequest(
            ILogger<LoggedGetAllArticlesModelsRequest> logger,
            IPipelineNode<
                IGetAllArticlesModelsRequestContract,
                IGetAllArticlesModelsResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllArticlesModelsResultContract> Ask(
            IGetAllArticlesModelsRequestContract input)
        {
            _logger.LogInformation($"GetAllArticlesModelsLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetAllArticlesModelsLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}