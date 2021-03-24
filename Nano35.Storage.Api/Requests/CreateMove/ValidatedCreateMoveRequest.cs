using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateMove
{
    public class CreateMoveValidatorErrorResult : 
        ICreateMoveErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateMoveRequest:
        PipeNodeBase<ICreateMoveRequestContract, ICreateMoveResultContract>
    {
        public ValidatedCreateMoveRequest(
            IPipeNode<ICreateMoveRequestContract, ICreateMoveResultContract> next) :
            base(next) { }

        public override async Task<ICreateMoveResultContract> Ask(
            ICreateMoveRequestContract input)
        {
            if (false)
            {
                return new CreateMoveValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}