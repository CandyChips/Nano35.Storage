using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllArticleModels
{
    public class GetAllArticlesModelsValidatorErrorResult : 
        IGetAllArticlesModelsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllArticlesModelsRequest:
        PipeNodeBase<IGetAllArticlesModelsRequestContract, IGetAllArticlesModelsResultContract>
    {
        public ValidatedGetAllArticlesModelsRequest(
            IPipeNode<IGetAllArticlesModelsRequestContract, IGetAllArticlesModelsResultContract> next) :
            base(next) { }

        public override async Task<IGetAllArticlesModelsResultContract> Ask(
            IGetAllArticlesModelsRequestContract input)
        {
            if (false)
            {
                return new GetAllArticlesModelsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}