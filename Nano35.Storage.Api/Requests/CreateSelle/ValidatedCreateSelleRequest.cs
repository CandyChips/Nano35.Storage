using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateSelle
{
    public class CreateSelleValidatorErrorResult : 
        ICreateSelleErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateSelleRequest:
        PipeNodeBase<ICreateSelleRequestContract, ICreateSelleResultContract>
    {

        public ValidatedCreateSelleRequest(
            IPipeNode<ICreateSelleRequestContract, ICreateSelleResultContract> next) :
            base(next) { }

        public override async Task<ICreateSelleResultContract> Ask(
            ICreateSelleRequestContract input)
        {
            if (false)
            {
                return new CreateSelleValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}