using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetSelleDetails
{
    public class LoggedGetSelleDetailsRequest :
        IPipelineNode<
            IGetSelleDetailsRequestContract, 
            IGetSelleDetailsResultContract>
    {
        private readonly ILogger<LoggedGetSelleDetailsRequest> _logger;
        private readonly IPipelineNode<
            IGetSelleDetailsRequestContract,
            IGetSelleDetailsResultContract> _nextNode;

        public LoggedGetSelleDetailsRequest(
            ILogger<LoggedGetSelleDetailsRequest> logger,
            IPipelineNode<
                IGetSelleDetailsRequestContract,
                IGetSelleDetailsResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetSelleDetailsResultContract> Ask(
            IGetSelleDetailsRequestContract input)
        {
            _logger.LogInformation($"GetSelleDetailsLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetSelleDetailsLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}