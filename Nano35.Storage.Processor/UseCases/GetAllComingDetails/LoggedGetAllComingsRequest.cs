using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllComingDetails
{
    public class LoggedGetAllComingDetailsRequest :
        IPipelineNode<
            IGetAllComingDetailsRequestContract, 
            IGetAllComingDetailsResultContract>
    {
        private readonly ILogger<LoggedGetAllComingDetailsRequest> _logger;
        private readonly IPipelineNode<
            IGetAllComingDetailsRequestContract,
            IGetAllComingDetailsResultContract> _nextNode;

        public LoggedGetAllComingDetailsRequest(
            ILogger<LoggedGetAllComingDetailsRequest> logger,
            IPipelineNode<
                IGetAllComingDetailsRequestContract,
                IGetAllComingDetailsResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllComingDetailsResultContract> Ask(
            IGetAllComingDetailsRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllComingDetailsLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllComingDetailsLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}