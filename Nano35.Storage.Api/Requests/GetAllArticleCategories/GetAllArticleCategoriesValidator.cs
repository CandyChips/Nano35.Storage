using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllArticleCategories
{
    public class GetAllArticlesCategoriesValidatorErrorResult : 
        IGetAllArticlesCategoriesErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class GetAllArticlesCategoriesValidator:
        IPipelineNode<
            IGetAllArticlesCategoriesRequestContract,
            IGetAllArticlesCategoriesResultContract>
    {
        private readonly IPipelineNode<
            IGetAllArticlesCategoriesRequestContract,
            IGetAllArticlesCategoriesResultContract> _nextNode;

        public GetAllArticlesCategoriesValidator(
            IPipelineNode<
                IGetAllArticlesCategoriesRequestContract,
                IGetAllArticlesCategoriesResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllArticlesCategoriesResultContract> Ask(
            IGetAllArticlesCategoriesRequestContract input)
        {
            if (false)
            {
                return new GetAllArticlesCategoriesValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}