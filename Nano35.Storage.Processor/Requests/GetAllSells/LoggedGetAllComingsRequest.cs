using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.GetAllSells
{
    public class LoggedGetAllSellsRequest :
        IPipelineNode<
            IGetAllSellsRequestContract, 
            IGetAllSellsResultContract>
    {
        private readonly ILogger<LoggedGetAllSellsRequest> _logger;
        private readonly IPipelineNode<
            IGetAllSellsRequestContract,
            IGetAllSellsResultContract> _nextNode;

        public LoggedGetAllSellsRequest(
            ILogger<LoggedGetAllSellsRequest> logger,
            IPipelineNode<
                IGetAllSellsRequestContract,
                IGetAllSellsResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllSellsResultContract> Ask(
            IGetAllSellsRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllSellsLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllSellsLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}