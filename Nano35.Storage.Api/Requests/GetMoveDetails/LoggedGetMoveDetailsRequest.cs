using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetMoveDetails
{
    public class LoggedGetMoveDetailsRequest :
        IPipelineNode<
            IGetMoveDetailsRequestContract, 
            IGetMoveDetailsResultContract>
    {
        private readonly ILogger<LoggedGetMoveDetailsRequest> _logger;
        private readonly IPipelineNode<
            IGetMoveDetailsRequestContract,
            IGetMoveDetailsResultContract> _nextNode;

        public LoggedGetMoveDetailsRequest(
            ILogger<LoggedGetMoveDetailsRequest> logger,
            IPipelineNode<
                IGetMoveDetailsRequestContract,
                IGetMoveDetailsResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetMoveDetailsResultContract> Ask(
            IGetMoveDetailsRequestContract input)
        {
            _logger.LogInformation($"GetMoveDetailsLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetMoveDetailsLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}