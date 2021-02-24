using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.GetAllWarehousesOfUnit
{
    public class LoggedGetAllWarehousesOfUnitRequest :
        IPipelineNode<
            IGetAllWarehousesOfUnitRequestContract, 
            IGetAllWarehousesOfUnitResultContract>
    {
        private readonly ILogger<LoggedGetAllWarehousesOfUnitRequest> _logger;
        private readonly IPipelineNode<
            IGetAllWarehousesOfUnitRequestContract,
            IGetAllWarehousesOfUnitResultContract> _nextNode;

        public LoggedGetAllWarehousesOfUnitRequest(
            ILogger<LoggedGetAllWarehousesOfUnitRequest> logger,
            IPipelineNode<
                IGetAllWarehousesOfUnitRequestContract,
                IGetAllWarehousesOfUnitResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllWarehousesOfUnitResultContract> Ask(
            IGetAllWarehousesOfUnitRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllWarehousesOfUnitLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllWarehousesOfUnitLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}