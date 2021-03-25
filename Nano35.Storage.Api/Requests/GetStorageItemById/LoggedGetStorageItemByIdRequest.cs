using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetStorageItemById
{
    public class LoggedGetStorageItemByIdRequest :
        PipeNodeBase<IGetStorageItemByIdRequestContract, IGetStorageItemByIdResultContract>
    {
        private readonly ILogger<LoggedGetStorageItemByIdRequest> _logger;

        public LoggedGetStorageItemByIdRequest(
            ILogger<LoggedGetStorageItemByIdRequest> logger,
            IPipeNode<IGetStorageItemByIdRequestContract, IGetStorageItemByIdResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetStorageItemByIdResultContract> Ask(
            IGetStorageItemByIdRequestContract input)
        {
            _logger.LogInformation($"GetStorageItemByIdLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"GetStorageItemByIdLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}