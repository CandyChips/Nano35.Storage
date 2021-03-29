using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateSelle
{
    public class CreateSelleRequest :
        MasstransitRequest<
            ICreateSelleRequestContract,
            ICreateSelleResultContract,
            ICreateSelleSuccessResultContract,
            ICreateSelleErrorResultContract>
    {
        public CreateSelleRequest(IBus bus) : base(bus) {}
    }
}