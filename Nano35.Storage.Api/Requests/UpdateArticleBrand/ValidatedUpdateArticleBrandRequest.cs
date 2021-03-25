using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateArticleBrand
{
    public class UpdateArticleBrandValidatorErrorResult : 
        IUpdateArticleBrandErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateArticleBrandRequest:
        PipeNodeBase<IUpdateArticleBrandRequestContract, IUpdateArticleBrandResultContract>
    {
        public ValidatedUpdateArticleBrandRequest(
            IPipeNode<IUpdateArticleBrandRequestContract, IUpdateArticleBrandResultContract> next) :
            base(next) { }

        public override async Task<IUpdateArticleBrandResultContract> Ask(
            IUpdateArticleBrandRequestContract input)
        {
            if (false)
            {
                return new UpdateArticleBrandValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}