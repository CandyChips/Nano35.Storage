using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllMoves
{
    public class LoggedGetAllMovesRequest :
        PipeNodeBase<
            IGetAllMovesRequestContract, 
            IGetAllMovesResultContract>
    {
        private readonly ILogger<LoggedGetAllMovesRequest> _logger;

        public LoggedGetAllMovesRequest(
            ILogger<LoggedGetAllMovesRequest> logger,
            IPipeNode<IGetAllMovesRequestContract,
                IGetAllMovesResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllMovesResultContract> Ask(
            IGetAllMovesRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllMovesLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"GetAllMovesLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}