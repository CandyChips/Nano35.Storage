using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemCondition
{
    public class LoggedUpdateStorageItemConditionRequest :
        IPipelineNode<
            IUpdateStorageItemConditionRequestContract, 
            IUpdateStorageItemConditionResultContract>
    {
        private readonly ILogger<LoggedUpdateStorageItemConditionRequest> _logger;
        private readonly IPipelineNode<
            IUpdateStorageItemConditionRequestContract,
            IUpdateStorageItemConditionResultContract> _nextNode;

        public LoggedUpdateStorageItemConditionRequest(
            ILogger<LoggedUpdateStorageItemConditionRequest> logger,
            IPipelineNode<
                IUpdateStorageItemConditionRequestContract, 
                IUpdateStorageItemConditionResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateStorageItemConditionResultContract> Ask(
            IUpdateStorageItemConditionRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"UpdateStorageItemConditionLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"UpdateStorageItemConditionLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}