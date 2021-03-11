using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllPlacesOnStorage
{
    public class LoggedGetAllPlacesOnStorageRequest :
        IPipelineNode<
            IGetAllPlacesOnStorageContract, 
            IGetAllPlacesOnStorageResultContract>
    {
        private readonly ILogger<LoggedGetAllPlacesOnStorageRequest> _logger;
        private readonly IPipelineNode<
            IGetAllPlacesOnStorageContract,
            IGetAllPlacesOnStorageResultContract> _nextNode;

        public LoggedGetAllPlacesOnStorageRequest(
            ILogger<LoggedGetAllPlacesOnStorageRequest> logger,
            IPipelineNode<
                IGetAllPlacesOnStorageContract,
                IGetAllPlacesOnStorageResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllPlacesOnStorageResultContract> Ask(
            IGetAllPlacesOnStorageContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllPlacesOnStorageLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllPlacesOnStorageLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}