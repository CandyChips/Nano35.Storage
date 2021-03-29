using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllWarehouseNames
{
    public class LoggedGetAllWarehouseNamesRequest :
        IPipelineNode<
            IGetAllWarehouseNamesRequestContract, 
            IGetAllWarehouseNamesResultContract>
    {
        private readonly ILogger<LoggedGetAllWarehouseNamesRequest> _logger;
        private readonly IPipelineNode<
            IGetAllWarehouseNamesRequestContract,
            IGetAllWarehouseNamesResultContract> _nextNode;

        public LoggedGetAllWarehouseNamesRequest(
            ILogger<LoggedGetAllWarehouseNamesRequest> logger,
            IPipelineNode<
                IGetAllWarehouseNamesRequestContract,
                IGetAllWarehouseNamesResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllWarehouseNamesResultContract> Ask(
            IGetAllWarehouseNamesRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllWarehouseNamesLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllWarehouseNamesLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}