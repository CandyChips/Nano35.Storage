using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllSelleDetails
{
    public class GetAllSelleDetailsValidatorErrorResult : 
        IGetAllSelleDetailsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllSelleDetailsRequest:
        PipeNodeBase<IGetAllSelleDetailsRequestContract, IGetAllSelleDetailsResultContract>
    {
        public ValidatedGetAllSelleDetailsRequest(
            IPipeNode<IGetAllSelleDetailsRequestContract, IGetAllSelleDetailsResultContract> next) :
            base(next) { }

        public override async Task<IGetAllSelleDetailsResultContract> Ask(
            IGetAllSelleDetailsRequestContract input)
        {
            if (false)
            {
                return new GetAllSelleDetailsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}