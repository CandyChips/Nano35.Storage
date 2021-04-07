using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemCondition
{
    public class LoggedUpdateStorageItemConditionRequest :
        PipeNodeBase<
            IUpdateStorageItemConditionRequestContract, 
            IUpdateStorageItemConditionResultContract>
    {
        private readonly ILogger<LoggedUpdateStorageItemConditionRequest> _logger;

        public LoggedUpdateStorageItemConditionRequest(
            ILogger<LoggedUpdateStorageItemConditionRequest> logger,
            IPipeNode<IUpdateStorageItemConditionRequestContract,
                IUpdateStorageItemConditionResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateStorageItemConditionResultContract> Ask(
            IUpdateStorageItemConditionRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"UpdateStorageItemConditionLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"UpdateStorageItemConditionLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}