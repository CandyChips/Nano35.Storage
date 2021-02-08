using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.GetAllArticleModels
{
    public class GetAllArticlesModelsValidatorErrorResult :
        IGetAllArticlesModelsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class GetAllArticlesModelsValidator:
        IPipelineNode<
            IGetAllArticlesModelsRequestContract, 
            IGetAllArticlesModelsResultContract>
    {
        private readonly IPipelineNode<
            IGetAllArticlesModelsRequestContract, 
            IGetAllArticlesModelsResultContract> _nextNode;

        public GetAllArticlesModelsValidator(
            IPipelineNode<
                IGetAllArticlesModelsRequestContract,
                IGetAllArticlesModelsResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllArticlesModelsResultContract> Ask(
            IGetAllArticlesModelsRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllArticlesModelsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}