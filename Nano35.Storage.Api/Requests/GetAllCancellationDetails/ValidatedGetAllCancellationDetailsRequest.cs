using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllCancellationDetails
{
    public class GetAllCancellationDetailsValidatorErrorResult : 
        IGetAllCancellationDetailsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllCancellationDetailsRequest:
        PipeNodeBase<IGetAllCancellationDetailsRequestContract, IGetAllCancellationDetailsResultContract>
    {
        public ValidatedGetAllCancellationDetailsRequest(
            IPipeNode<IGetAllCancellationDetailsRequestContract, IGetAllCancellationDetailsResultContract> next) :
            base(next) { }

        public override async Task<IGetAllCancellationDetailsResultContract> Ask(
            IGetAllCancellationDetailsRequestContract input)
        {
            if (false)
            {
                return new GetAllCancellationDetailsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}