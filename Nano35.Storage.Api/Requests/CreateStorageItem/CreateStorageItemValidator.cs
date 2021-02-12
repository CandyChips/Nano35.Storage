using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateStorageItem
{
    public class CreateStorageItemValidatorErrorResult : 
        ICreateStorageItemErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class CreateStorageItemValidator:
        IPipelineNode<
            ICreateStorageItemRequestContract, 
            ICreateStorageItemResultContract>
    {
        private readonly IPipelineNode<
            ICreateStorageItemRequestContract, 
            ICreateStorageItemResultContract> _nextNode;

        public CreateStorageItemValidator(
            IPipelineNode<
                ICreateStorageItemRequestContract, 
                ICreateStorageItemResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<ICreateStorageItemResultContract> Ask(
            ICreateStorageItemRequestContract input)
        {
            if (false)
            {
                return new CreateStorageItemValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}