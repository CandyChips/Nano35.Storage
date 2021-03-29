using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateMove
{
    public class CreateMoveRequest :
        MasstransitRequest<
            ICreateMoveRequestContract,
            ICreateMoveResultContract,
            ICreateMoveSuccessResultContract,
            ICreateMoveErrorResultContract>
    {
        public CreateMoveRequest(IBus bus) : base(bus) {}
    }
}