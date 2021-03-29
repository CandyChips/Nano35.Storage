using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllMoves
{
    public class GetAllMovesRequest : 
        MasstransitRequest<
            IGetAllMovesRequestContract,
            IGetAllMovesResultContract,
            IGetAllMovesSuccessResultContract,
            IGetAllMovesErrorResultContract>
    {
        public GetAllMovesRequest(IBus bus) : base(bus) {}
    }
}