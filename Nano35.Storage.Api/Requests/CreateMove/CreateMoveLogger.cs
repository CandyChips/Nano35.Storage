using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateMove
{
    public class CreateMoveLogger :
        IPipelineNode<ICreateMoveRequestContract, ICreateMoveResultContract>
    {
        private readonly ILogger<CreateMoveLogger> _logger;
        private readonly IPipelineNode<ICreateMoveRequestContract, ICreateMoveResultContract> _nextNode;

        public CreateMoveLogger(
            ILogger<CreateMoveLogger> logger,
            IPipelineNode<ICreateMoveRequestContract, ICreateMoveResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreateMoveResultContract> Ask(
            ICreateMoveRequestContract input)
        {
            _logger.LogInformation($"CreateMoveLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"CreateMoveLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}