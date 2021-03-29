using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateComing
{
    public class CreateComingRequest :
        MasstransitRequest<
            ICreateComingRequestContract,
            ICreateComingResultContract,
            ICreateComingSuccessResultContract,
            ICreateComingErrorResultContract>
    {
        public CreateComingRequest(IBus bus) : base(bus) {}
    }
}