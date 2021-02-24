using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemCondition
{
    public class UpdateStorageItemConditionValidatorErrorResult : 
        IUpdateStorageItemConditionErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateStorageItemConditionRequest:
        IPipelineNode<
            IUpdateStorageItemConditionRequestContract,
            IUpdateStorageItemConditionResultContract>
    {
        private readonly IPipelineNode<
            IUpdateStorageItemConditionRequestContract, 
            IUpdateStorageItemConditionResultContract> _nextNode;

        public ValidatedUpdateStorageItemConditionRequest(
            IPipelineNode<
                IUpdateStorageItemConditionRequestContract, 
                IUpdateStorageItemConditionResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateStorageItemConditionResultContract> Ask(
            IUpdateStorageItemConditionRequestContract input)
        {
            if (false)
            {
                return new UpdateStorageItemConditionValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}