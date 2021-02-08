using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.CreateCancelation
{
    public class CreateCancelationLogger :
        IPipelineNode<ICreateCancelationRequestContract, ICreateCancelationResultContract>
    {
        private readonly ILogger<CreateCancelationLogger> _logger;
        private readonly IPipelineNode<ICreateCancelationRequestContract, ICreateCancelationResultContract> _nextNode;

        public CreateCancelationLogger(
            ILogger<CreateCancelationLogger> logger,
            IPipelineNode<ICreateCancelationRequestContract, ICreateCancelationResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreateCancelationResultContract> Ask(
            ICreateCancelationRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateCancelationLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"CreateCancelationLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}