using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.GetCancellationDetailsById
{
    public class LoggedGetCancellationDetailsByIdRequest :
        IPipelineNode<
            IGetCancellationDetailsByIdRequestContract, 
            IGetCancellationDetailsByIdResultContract>
    {
        private readonly ILogger<LoggedGetCancellationDetailsByIdRequest> _logger;
        private readonly IPipelineNode<
            IGetCancellationDetailsByIdRequestContract,
            IGetCancellationDetailsByIdResultContract> _nextNode;

        public LoggedGetCancellationDetailsByIdRequest(
            ILogger<LoggedGetCancellationDetailsByIdRequest> logger,
            IPipelineNode<
                IGetCancellationDetailsByIdRequestContract,
                IGetCancellationDetailsByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetCancellationDetailsByIdResultContract> Ask(
            IGetCancellationDetailsByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetCancellationDetailsByIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetCancellationDetailsByIdLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}