using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllStorageItems
{
    public class GetAllStorageItemsValidatorErrorResult : 
        IGetAllStorageItemsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class GetAllStorageItemsValidator:
        IPipelineNode<
            IGetAllStorageItemsRequestContract,
            IGetAllStorageItemsResultContract>
    {
        private readonly IPipelineNode<
            IGetAllStorageItemsRequestContract, 
            IGetAllStorageItemsResultContract> _nextNode;

        public GetAllStorageItemsValidator(
            IPipelineNode<
                IGetAllStorageItemsRequestContract,
                IGetAllStorageItemsResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllStorageItemsResultContract> Ask(
            IGetAllStorageItemsRequestContract input)
        {
            if (false)
            {
                return new GetAllStorageItemsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}