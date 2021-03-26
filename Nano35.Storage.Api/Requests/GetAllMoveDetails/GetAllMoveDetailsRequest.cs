using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllMoveDetails
{
    public class GetAllMoveDetailsRequest : 
        MasstransitRequest<
            IGetAllMoveDetailsRequestContract,
            IGetAllMoveDetailsResultContract,
            IGetAllMoveDetailsSuccessResultContract,
            IGetAllMoveDetailsErrorResultContract>
    {
        public GetAllMoveDetailsRequest(IBus bus) : base(bus) {}
    }
}