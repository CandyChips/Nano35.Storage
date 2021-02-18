using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetCancellationDetails
{
    public class LoggedGetCancellationDetailsRequest :
        IPipelineNode<
            IGetCancellationDetailsRequestContract, 
            IGetCancellationDetailsResultContract>
    {
        private readonly ILogger<LoggedGetCancellationDetailsRequest> _logger;
        private readonly IPipelineNode<
            IGetCancellationDetailsRequestContract,
            IGetCancellationDetailsResultContract> _nextNode;

        public LoggedGetCancellationDetailsRequest(
            ILogger<LoggedGetCancellationDetailsRequest> logger,
            IPipelineNode<
                IGetCancellationDetailsRequestContract,
                IGetCancellationDetailsResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetCancellationDetailsResultContract> Ask(
            IGetCancellationDetailsRequestContract input)
        {
            _logger.LogInformation($"GetCancellationDetailsLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetCancellationDetailsLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}