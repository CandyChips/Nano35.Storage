using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetSelleDetails
{
    public class LoggedGetSelleDetailsRequest :
        IPipelineNode<
            IGetSelleDetailsByIdRequestContract, 
            IGetSelleDetailsByIdResultContract>
    {
        private readonly ILogger<LoggedGetSelleDetailsRequest> _logger;
        private readonly IPipelineNode<
            IGetSelleDetailsByIdRequestContract,
            IGetSelleDetailsByIdResultContract> _nextNode;

        public LoggedGetSelleDetailsRequest(
            ILogger<LoggedGetSelleDetailsRequest> logger,
            IPipelineNode<
                IGetSelleDetailsByIdRequestContract,
                IGetSelleDetailsByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetSelleDetailsByIdResultContract> Ask(
            IGetSelleDetailsByIdRequestContract input)
        {
            _logger.LogInformation($"GetSelleDetailsLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetSelleDetailsLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}