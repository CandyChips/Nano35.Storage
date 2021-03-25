using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateArticleInfo
{
    public class UpdateArticleInfoValidatorErrorResult : 
        IUpdateArticleInfoErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateArticleInfoRequest:
        PipeNodeBase<IUpdateArticleInfoRequestContract, IUpdateArticleInfoResultContract>
    {
        public ValidatedUpdateArticleInfoRequest(
            IPipeNode<IUpdateArticleInfoRequestContract, IUpdateArticleInfoResultContract> next) :
            base(next) { }

        public override async Task<IUpdateArticleInfoResultContract> Ask(
            IUpdateArticleInfoRequestContract input)
        {
            if (false)
            {
                return new UpdateArticleInfoValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}