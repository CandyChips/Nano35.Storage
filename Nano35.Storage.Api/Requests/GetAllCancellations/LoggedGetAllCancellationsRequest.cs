using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllCancellations
{
    public class LoggedGetAllCancellationsRequest :
        IPipelineNode<
            IGetAllCancellationsRequestContract, 
            IGetAllCancellationsResultContract>
    {
        private readonly ILogger<LoggedGetAllCancellationsRequest> _logger;
        private readonly IPipelineNode<
            IGetAllCancellationsRequestContract,
            IGetAllCancellationsResultContract> _nextNode;

        public LoggedGetAllCancellationsRequest(
            ILogger<LoggedGetAllCancellationsRequest> logger,
            IPipelineNode<
                IGetAllCancellationsRequestContract,
                IGetAllCancellationsResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllCancellationsResultContract> Ask(
            IGetAllCancellationsRequestContract input)
        {
            _logger.LogInformation($"GetAllCancellationsLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetAllCancellationsLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}