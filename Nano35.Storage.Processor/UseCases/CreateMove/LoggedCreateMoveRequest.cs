using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.CreateMove
{
    public class LoggedCreateMoveRequest :
        PipeNodeBase<
            ICreateMoveRequestContract,
            ICreateMoveResultContract>
    {
        private readonly ILogger<LoggedCreateMoveRequest> _logger;

        public LoggedCreateMoveRequest(
            ILogger<LoggedCreateMoveRequest> logger,
            IPipeNode<ICreateMoveRequestContract,
                ICreateMoveResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<ICreateMoveResultContract> Ask(
            ICreateMoveRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateMoveLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"CreateMoveLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}