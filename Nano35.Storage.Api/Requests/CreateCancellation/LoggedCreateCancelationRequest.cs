using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateCancellation
{
    public class LoggedCreateCancellationRequest :
        IPipelineNode<ICreateCancellationRequestContract, ICreateCancellationResultContract>
    {
        private readonly ILogger<LoggedCreateCancellationRequest> _logger;
        private readonly IPipelineNode<ICreateCancellationRequestContract, ICreateCancellationResultContract> _nextNode;

        public LoggedCreateCancellationRequest(
            ILogger<LoggedCreateCancellationRequest> logger,
            IPipelineNode<ICreateCancellationRequestContract, ICreateCancellationResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreateCancellationResultContract> Ask(
            ICreateCancellationRequestContract input)
        {
            _logger.LogInformation($"CreateCancellationLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"CreateCancellationLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}