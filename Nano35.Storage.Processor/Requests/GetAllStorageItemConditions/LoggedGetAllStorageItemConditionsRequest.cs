using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.GetAllStorageItemConditions
{
    public class LoggedGetAllStorageItemConditionsRequest :
        IPipelineNode<
            IGetAllStorageItemConditionsRequestContract, 
            IGetAllStorageItemConditionsResultContract>
    {
        private readonly ILogger<LoggedGetAllStorageItemConditionsRequest> _logger;
        private readonly IPipelineNode<
            IGetAllStorageItemConditionsRequestContract,
            IGetAllStorageItemConditionsResultContract> _nextNode;

        public LoggedGetAllStorageItemConditionsRequest(
            ILogger<LoggedGetAllStorageItemConditionsRequest> logger,
            IPipelineNode<
                IGetAllStorageItemConditionsRequestContract,
                IGetAllStorageItemConditionsResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllStorageItemConditionsResultContract> Ask(
            IGetAllStorageItemConditionsRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllStorageItemConditionsLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllStorageItemConditionsLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}