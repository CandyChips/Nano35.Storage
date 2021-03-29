using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllWarehouseNames
{
    public class LoggedGetAllWarehouseNamesRequest :
        PipeNodeBase<
            IGetAllWarehouseNamesRequestContract, 
            IGetAllWarehouseNamesResultContract>
    {
        private readonly ILogger<LoggedGetAllWarehouseNamesRequest> _logger;

        public LoggedGetAllWarehouseNamesRequest(
            ILogger<LoggedGetAllWarehouseNamesRequest> logger,
            IPipeNode<IGetAllWarehouseNamesRequestContract,
                IGetAllWarehouseNamesResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllWarehouseNamesResultContract> Ask(
            IGetAllWarehouseNamesRequestContract input)
        {
            _logger.LogInformation($"GetAllWarehouseNamesLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"GetAllWarehouseNamesLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}