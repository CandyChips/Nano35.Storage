using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllSelleDetails
{
    public class GetAllSelleDetailsRequest : 
        MasstransitRequest<
            IGetAllSelleDetailsRequestContract,
            IGetAllSelleDetailsResultContract,
            IGetAllSelleDetailsSuccessResultContract,
            IGetAllSelleDetailsErrorResultContract>
    {
        public GetAllSelleDetailsRequest(IBus bus) : base(bus) {}
    }
}