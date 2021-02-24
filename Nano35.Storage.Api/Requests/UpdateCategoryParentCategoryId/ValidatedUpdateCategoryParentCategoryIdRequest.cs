using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateCategoryParentCategoryId
{
    public class UpdateCategoryParentCategoryIdValidatorErrorResult : 
        IUpdateCategoryParentCategoryIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateCategoryParentCategoryIdRequest:
        IPipelineNode<
            IUpdateCategoryParentCategoryIdRequestContract,
            IUpdateCategoryParentCategoryIdResultContract>
    {
        private readonly IPipelineNode<
            IUpdateCategoryParentCategoryIdRequestContract, 
            IUpdateCategoryParentCategoryIdResultContract> _nextNode;

        public ValidatedUpdateCategoryParentCategoryIdRequest(
            IPipelineNode<
                IUpdateCategoryParentCategoryIdRequestContract, 
                IUpdateCategoryParentCategoryIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateCategoryParentCategoryIdResultContract> Ask(
            IUpdateCategoryParentCategoryIdRequestContract input)
        {
            if (false)
            {
                return new UpdateCategoryParentCategoryIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}