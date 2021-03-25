using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateArticleBrand
{
    public class LoggedUpdateArticleBrandRequest :
        PipeNodeBase<IUpdateArticleBrandRequestContract, IUpdateArticleBrandResultContract>
    {
        private readonly ILogger<LoggedUpdateArticleBrandRequest> _logger;

        public LoggedUpdateArticleBrandRequest(
            ILogger<LoggedUpdateArticleBrandRequest> logger,
            IPipeNode<IUpdateArticleBrandRequestContract, IUpdateArticleBrandResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateArticleBrandResultContract> Ask(
            IUpdateArticleBrandRequestContract input)
        {
            _logger.LogInformation($"UpdateArticleBrandLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"UpdateArticleBrandLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}