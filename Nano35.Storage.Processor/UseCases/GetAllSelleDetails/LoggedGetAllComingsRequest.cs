using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllSelleDetails
{
    public class LoggedGetAllSelleDetailsRequest :
        IPipelineNode<
            IGetAllSelleDetailsRequestContract, 
            IGetAllSelleDetailsResultContract>
    {
        private readonly ILogger<LoggedGetAllSelleDetailsRequest> _logger;
        private readonly IPipelineNode<
            IGetAllSelleDetailsRequestContract,
            IGetAllSelleDetailsResultContract> _nextNode;

        public LoggedGetAllSelleDetailsRequest(
            ILogger<LoggedGetAllSelleDetailsRequest> logger,
            IPipelineNode<
                IGetAllSelleDetailsRequestContract,
                IGetAllSelleDetailsResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllSelleDetailsResultContract> Ask(
            IGetAllSelleDetailsRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllSelleDetailsLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllSelleDetailsLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}