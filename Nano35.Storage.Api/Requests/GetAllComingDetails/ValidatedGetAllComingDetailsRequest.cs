using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllComingDetails
{
    public class ValidatedGetAllComingDetailsRequest:
        PipeNodeBase<IGetAllComingDetailsRequestContract, IGetAllComingDetailsResultContract>
    {
        public ValidatedGetAllComingDetailsRequest(
            IPipeNode<IGetAllComingDetailsRequestContract, IGetAllComingDetailsResultContract> next) :
            base(next) { }

        public override async Task<IGetAllComingDetailsResultContract> Ask(
            IGetAllComingDetailsRequestContract input)
        {
            return await DoNext(input);
        }
    }
}