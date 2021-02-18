using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.GetSelleDetailsById
{
    public class LoggedGetSelleDetailsByIdRequest :
        IPipelineNode<
            IGetSelleDetailsByIdRequestContract, 
            IGetSelleDetailsByIdResultContract>
    {
        private readonly ILogger<LoggedGetSelleDetailsByIdRequest> _logger;
        private readonly IPipelineNode<
            IGetSelleDetailsByIdRequestContract,
            IGetSelleDetailsByIdResultContract> _nextNode;

        public LoggedGetSelleDetailsByIdRequest(
            ILogger<LoggedGetSelleDetailsByIdRequest> logger,
            IPipelineNode<
                IGetSelleDetailsByIdRequestContract,
                IGetSelleDetailsByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetSelleDetailsByIdResultContract> Ask(
            IGetSelleDetailsByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetSelleDetailsByIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetSelleDetailsByIdLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}