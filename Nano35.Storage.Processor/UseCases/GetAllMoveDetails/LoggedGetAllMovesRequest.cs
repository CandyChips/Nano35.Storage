using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllMoveDetails
{
    public class LoggedGetAllMoveDetailsRequest :
        IPipelineNode<
            IGetAllMoveDetailsRequestContract, 
            IGetAllMoveDetailsResultContract>
    {
        private readonly ILogger<LoggedGetAllMoveDetailsRequest> _logger;
        private readonly IPipelineNode<
            IGetAllMoveDetailsRequestContract,
            IGetAllMoveDetailsResultContract> _nextNode;

        public LoggedGetAllMoveDetailsRequest(
            ILogger<LoggedGetAllMoveDetailsRequest> logger,
            IPipelineNode<
                IGetAllMoveDetailsRequestContract,
                IGetAllMoveDetailsResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllMoveDetailsResultContract> Ask(
            IGetAllMoveDetailsRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllMoveDetailsLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllMoveDetailsLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}