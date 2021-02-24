using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.GetAllWarehousesOfItem
{
    public class LoggedGetAllWarehousesOfItemRequest :
        IPipelineNode<
            IGetAllWarehousesOfItemRequestContract, 
            IGetAllWarehousesOfItemResultContract>
    {
        private readonly ILogger<LoggedGetAllWarehousesOfItemRequest> _logger;
        private readonly IPipelineNode<
            IGetAllWarehousesOfItemRequestContract,
            IGetAllWarehousesOfItemResultContract> _nextNode;

        public LoggedGetAllWarehousesOfItemRequest(
            ILogger<LoggedGetAllWarehousesOfItemRequest> logger,
            IPipelineNode<
                IGetAllWarehousesOfItemRequestContract,
                IGetAllWarehousesOfItemResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllWarehousesOfItemResultContract> Ask(
            IGetAllWarehousesOfItemRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllWarehousesOfItemLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllWarehousesOfItemLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}