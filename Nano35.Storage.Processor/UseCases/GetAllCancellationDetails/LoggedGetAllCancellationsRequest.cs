using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllCancellationDetails
{
    public class LoggedGetAllCancellationDetailsRequest :
        IPipelineNode<
            IGetAllCancellationDetailsRequestContract, 
            IGetAllCancellationDetailsResultContract>
    {
        private readonly ILogger<LoggedGetAllCancellationDetailsRequest> _logger;
        private readonly IPipelineNode<
            IGetAllCancellationDetailsRequestContract,
            IGetAllCancellationDetailsResultContract> _nextNode;

        public LoggedGetAllCancellationDetailsRequest(
            ILogger<LoggedGetAllCancellationDetailsRequest> logger,
            IPipelineNode<
                IGetAllCancellationDetailsRequestContract,
                IGetAllCancellationDetailsResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllCancellationDetailsResultContract> Ask(
            IGetAllCancellationDetailsRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllCancellationDetailsLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllCancellationDetailsLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}