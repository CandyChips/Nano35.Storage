using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateArticleModel
{
    public class UpdateArticleModelValidatorErrorResult : 
        IUpdateArticleModelErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateArticleModelRequest:
        IPipelineNode<
            IUpdateArticleModelRequestContract,
            IUpdateArticleModelResultContract>
    {
        private readonly IPipelineNode<
            IUpdateArticleModelRequestContract, 
            IUpdateArticleModelResultContract> _nextNode;

        public ValidatedUpdateArticleModelRequest(
            IPipelineNode<
                IUpdateArticleModelRequestContract, 
                IUpdateArticleModelResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateArticleModelResultContract> Ask(
            IUpdateArticleModelRequestContract input)
        {
            if (false)
            {
                return new UpdateArticleModelValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}