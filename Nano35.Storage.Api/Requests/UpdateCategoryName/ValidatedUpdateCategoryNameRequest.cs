using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateCategoryName
{
    public class UpdateCategoryNameValidatorErrorResult : 
        IUpdateCategoryNameErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateCategoryNameRequest:
        IPipelineNode<
            IUpdateCategoryNameRequestContract,
            IUpdateCategoryNameResultContract>
    {
        private readonly IPipelineNode<
            IUpdateCategoryNameRequestContract, 
            IUpdateCategoryNameResultContract> _nextNode;

        public ValidatedUpdateCategoryNameRequest(
            IPipelineNode<
                IUpdateCategoryNameRequestContract, 
                IUpdateCategoryNameResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateCategoryNameResultContract> Ask(
            IUpdateCategoryNameRequestContract input)
        {
            if (false)
            {
                return new UpdateCategoryNameValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}