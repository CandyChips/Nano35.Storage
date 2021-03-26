using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllComingDetails
{
    public class GetAllComingDetailsRequest :
        MasstransitRequest<
            IGetAllComingDetailsRequestContract,
            IGetAllComingDetailsResultContract,
            IGetAllComingDetailsSuccessResultContract,
            IGetAllComingDetailsErrorResultContract>
    {
        public GetAllComingDetailsRequest(IBus bus) : base(bus) {}
    }
}