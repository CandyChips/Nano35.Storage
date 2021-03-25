using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllComingDetails
{
    public class GetAllComingDetailsValidatorErrorResult : 
        IGetAllComingDetailsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllComingDetailsRequest:
        PipeNodeBase<IGetAllComingDetailsRequestContract, IGetAllComingDetailsResultContract>
    {
        public ValidatedGetAllComingDetailsRequest(
            IPipeNode<IGetAllComingDetailsRequestContract, IGetAllComingDetailsResultContract> next) :
            base(next) { }

        public override async Task<IGetAllComingDetailsResultContract> Ask(
            IGetAllComingDetailsRequestContract input)
        {
            if (false)
            {
                return new GetAllComingDetailsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}