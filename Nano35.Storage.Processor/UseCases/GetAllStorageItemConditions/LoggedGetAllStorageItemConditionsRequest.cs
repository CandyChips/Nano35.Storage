using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllStorageItemConditions
{
    public class LoggedGetAllStorageItemConditionsRequest :
        PipeNodeBase<
            IGetAllStorageItemConditionsRequestContract, 
            IGetAllStorageItemConditionsResultContract>
    {
        private readonly ILogger<LoggedGetAllStorageItemConditionsRequest> _logger;

        public LoggedGetAllStorageItemConditionsRequest(
            ILogger<LoggedGetAllStorageItemConditionsRequest> logger,
            IPipeNode<IGetAllStorageItemConditionsRequestContract,
                IGetAllStorageItemConditionsResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllStorageItemConditionsResultContract> Ask(
            IGetAllStorageItemConditionsRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllStorageItemConditionsLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"GetAllStorageItemConditionsLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}