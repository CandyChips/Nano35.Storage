using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllStorageItemConditions
{
    public class GetAllStorageItemConditionsLogger :
        IPipelineNode<IGetAllStorageItemConditionsRequestContract, IGetAllStorageItemConditionsResultContract>
    {
        private readonly ILogger<GetAllStorageItemConditionsLogger> _logger;
        private readonly IPipelineNode<IGetAllStorageItemConditionsRequestContract, IGetAllStorageItemConditionsResultContract> _nextNode;

        public GetAllStorageItemConditionsLogger(
            ILogger<GetAllStorageItemConditionsLogger> logger,
            IPipelineNode<IGetAllStorageItemConditionsRequestContract, IGetAllStorageItemConditionsResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllStorageItemConditionsResultContract> Ask(
            IGetAllStorageItemConditionsRequestContract input)
        {
            _logger.LogInformation($"GetAllStorageItemConditionsLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetAllStorageItemConditionsLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}