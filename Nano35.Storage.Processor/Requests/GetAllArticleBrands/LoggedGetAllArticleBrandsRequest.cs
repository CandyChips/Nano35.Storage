using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.GetAllArticleBrands
{
    public class LoggedGetAllArticlesBrandsRequest :
        IPipelineNode<
            IGetAllArticlesBrandsRequestContract, 
            IGetAllArticlesBrandsResultContract>
    {
        private readonly ILogger<LoggedGetAllArticlesBrandsRequest> _logger;
        private readonly IPipelineNode<
            IGetAllArticlesBrandsRequestContract,
            IGetAllArticlesBrandsResultContract> _nextNode;

        public LoggedGetAllArticlesBrandsRequest(
            ILogger<LoggedGetAllArticlesBrandsRequest> logger,
            IPipelineNode<
                IGetAllArticlesBrandsRequestContract,
                IGetAllArticlesBrandsResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllArticlesBrandsResultContract> Ask(
            IGetAllArticlesBrandsRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllArticlesBrandsLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllArticlesBrandsLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}