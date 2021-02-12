using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.CreateSalle
{
    public class CreateSelleLogger :
        IPipelineNode<
            ICreateSelleRequestContract, 
            ICreateSelleResultContract>
    {
        private readonly ILogger<CreateSelleLogger> _logger;
        private readonly IPipelineNode<
            ICreateSelleRequestContract,
            ICreateSelleResultContract> _nextNode;

        public CreateSelleLogger(
            ILogger<CreateSelleLogger> logger,
            IPipelineNode<
                ICreateSelleRequestContract, 
                ICreateSelleResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreateSelleResultContract> Ask(
            ICreateSelleRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateSelleLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"CreateSelleLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}