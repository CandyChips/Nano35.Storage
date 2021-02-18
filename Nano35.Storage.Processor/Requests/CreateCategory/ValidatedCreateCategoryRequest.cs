using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.CreateCategory
{
    public class CreateCategoryValidatorErrorResult : ICreateCategoryErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateCategoryRequest:
        IPipelineNode<ICreateCategoryRequestContract, ICreateCategoryResultContract>
    {
        private readonly IPipelineNode<ICreateCategoryRequestContract, ICreateCategoryResultContract> _nextNode;

        public ValidatedCreateCategoryRequest(
            IPipelineNode<ICreateCategoryRequestContract, ICreateCategoryResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<ICreateCategoryResultContract> Ask(
            ICreateCategoryRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new CreateCategoryValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}