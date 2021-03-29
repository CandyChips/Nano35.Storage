using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllWarehouseOfStorageItem
{
    public class LoggedGetAllWarehouseOfStorageItemRequest :
        PipeNodeBase<
            IGetAllWarehouseOfStorageItemRequestContract, 
            IGetAllWarehouseOfStorageItemResultContract>
    {
        private readonly ILogger<LoggedGetAllWarehouseOfStorageItemRequest> _logger;

        public LoggedGetAllWarehouseOfStorageItemRequest(
            ILogger<LoggedGetAllWarehouseOfStorageItemRequest> logger,
            IPipeNode<IGetAllWarehouseOfStorageItemRequestContract,
                IGetAllWarehouseOfStorageItemResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllWarehouseOfStorageItemResultContract> Ask(
            IGetAllWarehouseOfStorageItemRequestContract input)
        {
            _logger.LogInformation($"GetAllWarehouseOfStorageItemLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"GetAllWarehouseOfStorageItemLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}