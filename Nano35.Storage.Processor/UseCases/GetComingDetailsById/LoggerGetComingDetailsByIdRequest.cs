using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetComingDetailsById
{
    public class LoggedGetComingDetailsByIdRequest :
        IPipelineNode<
            IGetComingDetailsByIdRequestContract, 
            IGetComingDetailsByIdResultContract>
    {
        private readonly ILogger<LoggedGetComingDetailsByIdRequest> _logger;
        private readonly IPipelineNode<
            IGetComingDetailsByIdRequestContract,
            IGetComingDetailsByIdResultContract> _nextNode;

        public LoggedGetComingDetailsByIdRequest(
            ILogger<LoggedGetComingDetailsByIdRequest> logger,
            IPipelineNode<
                IGetComingDetailsByIdRequestContract,
                IGetComingDetailsByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetComingDetailsByIdResultContract> Ask(
            IGetComingDetailsByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetComingDetailsByIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetComingDetailsByIdLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}