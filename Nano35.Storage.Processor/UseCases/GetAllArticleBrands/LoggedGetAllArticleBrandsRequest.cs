using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllArticleBrands
{
    public class LoggedGetAllArticlesBrandsRequest :
        PipeNodeBase<
            IGetAllArticlesBrandsRequestContract, 
            IGetAllArticlesBrandsResultContract>
    {
        private readonly ILogger<LoggedGetAllArticlesBrandsRequest> _logger;

        public LoggedGetAllArticlesBrandsRequest(
            ILogger<LoggedGetAllArticlesBrandsRequest> logger,
            IPipeNode<IGetAllArticlesBrandsRequestContract,
                IGetAllArticlesBrandsResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllArticlesBrandsResultContract> Ask(
            IGetAllArticlesBrandsRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllArticlesBrandsLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"GetAllArticlesBrandsLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}