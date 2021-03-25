using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetComingDetailsById
{
    public class GetComingDetailsByIdValidatorErrorResult :
        IGetComingDetailsByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetComingDetailsByIdRequest:
        PipeNodeBase<IGetComingDetailsByIdRequestContract, IGetComingDetailsByIdResultContract>
    {
        public ValidatedGetComingDetailsByIdRequest(
            IPipeNode<IGetComingDetailsByIdRequestContract, IGetComingDetailsByIdResultContract> next) :
            base(next) { }

        public override async Task<IGetComingDetailsByIdResultContract> Ask(
            IGetComingDetailsByIdRequestContract input)
        {
            return await DoNext(input);
        }
    }
}