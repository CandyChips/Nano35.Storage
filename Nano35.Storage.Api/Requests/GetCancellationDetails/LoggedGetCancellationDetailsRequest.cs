using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetCancellationDetails
{
    public class LoggedGetCancellationDetailsRequest :
        IPipelineNode<
            IGetCancellationDetailsByIdRequestContract, 
            IGetCancellationDetailsByIdResultContract>
    {
        private readonly ILogger<LoggedGetCancellationDetailsRequest> _logger;
        private readonly IPipelineNode<
            IGetCancellationDetailsByIdRequestContract,
            IGetCancellationDetailsByIdResultContract> _nextNode;

        public LoggedGetCancellationDetailsRequest(
            ILogger<LoggedGetCancellationDetailsRequest> logger,
            IPipelineNode<
                IGetCancellationDetailsByIdRequestContract,
                IGetCancellationDetailsByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetCancellationDetailsByIdResultContract> Ask(
            IGetCancellationDetailsByIdRequestContract input)
        {
            _logger.LogInformation($"GetCancellationDetailsLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetCancellationDetailsLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}