using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateCancellation
{
    public class CreateCancellationRequest :
        MasstransitRequest<
            ICreateCancellationRequestContract,
            ICreateCancellationResultContract,
            ICreateCancellationSuccessResultContract,
            ICreateCancellationErrorResultContract>
    {
        public CreateCancellationRequest(IBus bus) : base(bus) {}
    }
}