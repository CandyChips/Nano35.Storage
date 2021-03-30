using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllStorageItemsOnUnit
{
    public class LoggedGetAllStorageItemsOnUnitRequest :
        PipeNodeBase<
            IGetAllStorageItemsOnUnitContract, 
            IGetAllStorageItemsOnUnitResultContract>
    {
        private readonly ILogger<LoggedGetAllStorageItemsOnUnitRequest> _logger;
        public LoggedGetAllStorageItemsOnUnitRequest(
            ILogger<LoggedGetAllStorageItemsOnUnitRequest> logger,
            IPipeNode<IGetAllStorageItemsOnUnitContract,
                IGetAllStorageItemsOnUnitResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllStorageItemsOnUnitResultContract> Ask(
            IGetAllStorageItemsOnUnitContract input)
        {
            _logger.LogInformation($"GetAllStorageItemsOnUnitLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"GetAllStorageItemsOnUnitLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}