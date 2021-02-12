using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllComings
{
    public class GetAllComingsLogger :
        IPipelineNode<
            IGetAllComingsRequestContract, 
            IGetAllComingsResultContract>
    {
        private readonly ILogger<GetAllComingsLogger> _logger;
        private readonly IPipelineNode<
            IGetAllComingsRequestContract,
            IGetAllComingsResultContract> _nextNode;

        public GetAllComingsLogger(
            ILogger<GetAllComingsLogger> logger,
            IPipelineNode<
                IGetAllComingsRequestContract,
                IGetAllComingsResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllComingsResultContract> Ask(
            IGetAllComingsRequestContract input)
        {
            _logger.LogInformation($"GetAllComingsLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetAllComingsLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}