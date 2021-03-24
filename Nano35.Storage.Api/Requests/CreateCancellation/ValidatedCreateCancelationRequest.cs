using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateCancellation
{
    public class CreateCancellationValidatorErrorResult :
        ICreateCancellationErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateCancellationRequest :
        PipeNodeBase<ICreateCancellationRequestContract, ICreateCancellationResultContract>
    {
        public ValidatedCreateCancellationRequest(
            IPipeNode<ICreateCancellationRequestContract, ICreateCancellationResultContract> next) :
            base(next) { }

        public override async Task<ICreateCancellationResultContract> Ask(
            ICreateCancellationRequestContract input)
        {
            if (false)
            {
                return new CreateCancellationValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}