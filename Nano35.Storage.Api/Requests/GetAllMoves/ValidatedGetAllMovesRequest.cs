using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllMoves
{
    public class ValidatedGetAllMovesRequest:
        PipeNodeBase<IGetAllMovesRequestContract, IGetAllMovesResultContract>
    {
        public ValidatedGetAllMovesRequest(
            IPipeNode<IGetAllMovesRequestContract, IGetAllMovesResultContract> next) :
            base(next) { }

        public override async Task<IGetAllMovesResultContract> Ask(
            IGetAllMovesRequestContract input)
        {
            return await DoNext(input);
        }
    }
}