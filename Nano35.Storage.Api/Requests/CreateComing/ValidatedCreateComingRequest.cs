using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateComing
{
    public class CreateComingValidatorErrorResult : 
        ICreateComingErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateComingRequest:
        PipeNodeBase<ICreateComingRequestContract, ICreateComingResultContract>
    {

        public ValidatedCreateComingRequest(
            IPipeNode<ICreateComingRequestContract, ICreateComingResultContract> next) :
            base(next) { }
        
        public override async Task<ICreateComingResultContract> Ask(
            ICreateComingRequestContract input)
        {
            if (false)
            {
                return new CreateComingValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}