using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateCategory
{
    public class LoggedCreateCategoryRequest :
        PipeNodeBase<ICreateCategoryRequestContract, ICreateCategoryResultContract>
    {
        private readonly ILogger<LoggedCreateCategoryRequest> _logger;

        public LoggedCreateCategoryRequest(
            ILogger<LoggedCreateCategoryRequest> logger,
            IPipeNode<ICreateCategoryRequestContract, ICreateCategoryResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<ICreateCategoryResultContract> Ask(
            ICreateCategoryRequestContract input)
        {
            _logger.LogInformation($"CreateCategoryLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"CreateCategoryLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}