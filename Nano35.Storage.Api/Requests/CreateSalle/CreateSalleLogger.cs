using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateSalle
{
    public class CreateSalleLogger :
        IPipelineNode<ICreateSalleRequestContract, ICreateSalleResultContract>
    {
        private readonly ILogger<CreateSalleLogger> _logger;
        private readonly IPipelineNode<ICreateSalleRequestContract, ICreateSalleResultContract> _nextNode;

        public CreateSalleLogger(
            ILogger<CreateSalleLogger> logger,
            IPipelineNode<ICreateSalleRequestContract, ICreateSalleResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreateSalleResultContract> Ask(
            ICreateSalleRequestContract input)
        {
            _logger.LogInformation($"CreateSalleLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"CreateSalleLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}