using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllArticleBrands
{
    public class GetAllArticlesBrandsValidatorErrorResult : 
        IGetAllArticlesBrandsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class GetAllArticlesBrandsValidator:
        IPipelineNode<
            IGetAllArticlesBrandsRequestContract, 
            IGetAllArticlesBrandsResultContract>
    {
        private readonly IPipelineNode<
            IGetAllArticlesBrandsRequestContract,
            IGetAllArticlesBrandsResultContract> _nextNode;

        public GetAllArticlesBrandsValidator(
            IPipelineNode<
                IGetAllArticlesBrandsRequestContract, 
                IGetAllArticlesBrandsResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllArticlesBrandsResultContract> Ask(
            IGetAllArticlesBrandsRequestContract input)
        {
            if (false)
            {
                return new GetAllArticlesBrandsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}