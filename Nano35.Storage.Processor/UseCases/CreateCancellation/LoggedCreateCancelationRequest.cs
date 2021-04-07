using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.CreateCancellation
{
    public class LoggedCreateCancellatioRequest :
        PipeNodeBase<
            ICreateCancellationRequestContract,
            ICreateCancellationResultContract>
    {
        private readonly ILogger<LoggedCreateCancellatioRequest> _logger;

        public LoggedCreateCancellatioRequest(
            ILogger<LoggedCreateCancellatioRequest> logger,
            IPipeNode<ICreateCancellationRequestContract,
                ICreateCancellationResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<ICreateCancellationResultContract> Ask(
            ICreateCancellationRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Create cancellation logger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"Create cancellation logger ends on: {DateTime.Now}");
            return result;
        }
    }
}