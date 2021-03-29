using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllCancellations
{
    public class GetAllCancellationsRequest :
        MasstransitRequest<
            IGetAllCancellationsRequestContract,
            IGetAllCancellationsResultContract,
            IGetAllCancellationsSuccessResultContract,
            IGetAllCancellationsErrorResultContract>
    {
        public GetAllCancellationsRequest(IBus bus) : base(bus) {}
    }
}