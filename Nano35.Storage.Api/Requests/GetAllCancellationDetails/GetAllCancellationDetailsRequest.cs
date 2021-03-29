using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllCancellationDetails
{
    public class GetAllCancellationDetailsRequest : 
        MasstransitRequest<
            IGetAllCancellationDetailsRequestContract,
            IGetAllCancellationDetailsResultContract,
            IGetAllCancellationDetailsSuccessResultContract,
            IGetAllCancellationDetailsErrorResultContract>
    {
        public GetAllCancellationDetailsRequest(IBus bus) : base(bus) {}
    }
}