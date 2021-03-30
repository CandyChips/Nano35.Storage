using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllPlacesOfStorageItemOnUnit
{
    public class LoggedGetAllPlacesOfStorageItemOnUnitRequest :
        PipeNodeBase<
            IGetAllPlacesOfStorageItemOnUnitRequestContract, 
            IGetAllPlacesOfStorageItemOnUnitResultContract>
    {
        private readonly ILogger<LoggedGetAllPlacesOfStorageItemOnUnitRequest> _logger;

        public LoggedGetAllPlacesOfStorageItemOnUnitRequest(
            ILogger<LoggedGetAllPlacesOfStorageItemOnUnitRequest> logger,
            IPipeNode<IGetAllPlacesOfStorageItemOnUnitRequestContract,
                IGetAllPlacesOfStorageItemOnUnitResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllPlacesOfStorageItemOnUnitResultContract> Ask(
            IGetAllPlacesOfStorageItemOnUnitRequestContract input)
        {
            _logger.LogInformation($"GetAllPlacesOfStorageItemOnUnitLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"GetAllPlacesOfStorageItemOnUnitLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}