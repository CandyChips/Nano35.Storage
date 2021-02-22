using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetMoveDetails
{
    public class LoggedGetMoveDetailsByIdRequest :
        IPipelineNode<
            IGetMoveDetailsByIdRequestContract, 
            IGetMoveDetailsByIdResultContract>
    {
        private readonly ILogger<LoggedGetMoveDetailsByIdRequest> _logger;
        private readonly IPipelineNode<
            IGetMoveDetailsByIdRequestContract,
            IGetMoveDetailsByIdResultContract> _nextNode;

        public LoggedGetMoveDetailsByIdRequest(
            ILogger<LoggedGetMoveDetailsByIdRequest> logger,
            IPipelineNode<
                IGetMoveDetailsByIdRequestContract,
                IGetMoveDetailsByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetMoveDetailsByIdResultContract> Ask(
            IGetMoveDetailsByIdRequestContract input)
        {
            _logger.LogInformation($"GetMoveDetailsByIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetMoveDetailsByIdLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}