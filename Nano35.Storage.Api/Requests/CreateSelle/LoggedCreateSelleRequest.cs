using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateSelle
{
    public class LoggedCreateSelleRequest :
        IPipelineNode<
            ICreateSelleRequestContract, 
            ICreateSelleResultContract>
    {
        private readonly ILogger<LoggedCreateSelleRequest> _logger;
        private readonly IPipelineNode<
            ICreateSelleRequestContract, 
            ICreateSelleResultContract> _nextNode;

        public LoggedCreateSelleRequest(
            ILogger<LoggedCreateSelleRequest> logger,
            IPipelineNode<
                ICreateSelleRequestContract, 
                ICreateSelleResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreateSelleResultContract> Ask(
            ICreateSelleRequestContract input)
        {
            _logger.LogInformation($"CreateSelleLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"CreateSelleLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}