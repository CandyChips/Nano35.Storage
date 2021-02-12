using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateCategory
{
    public class CreateCategoryValidatorErrorResult : 
        ICreateCategoryErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class CreateCategoryValidator:
        IPipelineNode<
            ICreateCategoryRequestContract, 
            ICreateCategoryResultContract>
    {
        private readonly IPipelineNode<
            ICreateCategoryRequestContract, 
            ICreateCategoryResultContract> _nextNode;

        public CreateCategoryValidator(
            IPipelineNode<
                ICreateCategoryRequestContract, 
                ICreateCategoryResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<ICreateCategoryResultContract> Ask(
            ICreateCategoryRequestContract input)
        {
            if (false)
            {
                return new CreateCategoryValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}