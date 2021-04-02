using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllMoveDetails
{
    public class ValidatedGetAllMoveDetailsRequest:
        PipeNodeBase<IGetAllMoveDetailsRequestContract, IGetAllMoveDetailsResultContract>
    {
        public ValidatedGetAllMoveDetailsRequest(
            IPipeNode<IGetAllMoveDetailsRequestContract, IGetAllMoveDetailsResultContract> next) :
            base(next) { }

        public override async Task<IGetAllMoveDetailsResultContract> Ask(
            IGetAllMoveDetailsRequestContract input)
        {
            return await DoNext(input);
        }
    }
}