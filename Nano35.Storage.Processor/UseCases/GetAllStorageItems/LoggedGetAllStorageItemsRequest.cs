using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllStorageItems
{
    public class LoggedGetAllStorageItemsRequest :
        PipeNodeBase<
            IGetAllStorageItemsRequestContract, 
            IGetAllStorageItemsResultContract>
    {
        private readonly ILogger<LoggedGetAllStorageItemsRequest> _logger;

        public LoggedGetAllStorageItemsRequest(
            ILogger<LoggedGetAllStorageItemsRequest> logger,
            IPipeNode<IGetAllStorageItemsRequestContract,
                IGetAllStorageItemsResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllStorageItemsResultContract> Ask(
            IGetAllStorageItemsRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllStorageItemsLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"GetAllStorageItemsLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}