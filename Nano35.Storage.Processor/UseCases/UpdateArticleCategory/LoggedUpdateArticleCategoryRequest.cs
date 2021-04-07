using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.UpdateArticleCategory
{
    public class LoggedUpdateArticleCategoryRequest :
        PipeNodeBase<
            IUpdateArticleCategoryRequestContract, 
            IUpdateArticleCategoryResultContract>
    {
        private readonly ILogger<LoggedUpdateArticleCategoryRequest> _logger;

        public LoggedUpdateArticleCategoryRequest(
            ILogger<LoggedUpdateArticleCategoryRequest> logger,
            IPipeNode<IUpdateArticleCategoryRequestContract,
                IUpdateArticleCategoryResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateArticleCategoryResultContract> Ask(
            IUpdateArticleCategoryRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"UpdateArticleCategoryLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"UpdateArticleCategoryLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}