using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.UpdateCategoryName
{
    public class LoggedUpdateCategoryNameRequest :
        PipeNodeBase<
            IUpdateCategoryNameRequestContract, 
            IUpdateCategoryNameResultContract>
    {
        private readonly ILogger<LoggedUpdateCategoryNameRequest> _logger;

        public LoggedUpdateCategoryNameRequest(
            ILogger<LoggedUpdateCategoryNameRequest> logger,
            IPipeNode<IUpdateCategoryNameRequestContract,
                IUpdateCategoryNameResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateCategoryNameResultContract> Ask(
            IUpdateCategoryNameRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"UpdateCategoryNameLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"UpdateCategoryNameLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}