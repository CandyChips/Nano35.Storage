using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateCategory
{
    public class LoggedCreateCategoryRequest :
        IPipelineNode<
            ICreateCategoryRequestContract, 
            ICreateCategoryResultContract>
    {
        private readonly ILogger<LoggedCreateCategoryRequest> _logger;
        private readonly IPipelineNode<
            ICreateCategoryRequestContract, 
            ICreateCategoryResultContract> _nextNode;

        public LoggedCreateCategoryRequest(
            ILogger<LoggedCreateCategoryRequest> logger,
            IPipelineNode<
                ICreateCategoryRequestContract, 
                ICreateCategoryResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreateCategoryResultContract> Ask(
            ICreateCategoryRequestContract input)
        {
            _logger.LogInformation($"CreateCategoryLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"CreateCategoryLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}