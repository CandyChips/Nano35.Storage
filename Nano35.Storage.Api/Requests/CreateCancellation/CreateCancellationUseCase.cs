using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateCancellation
{
    public class CreateCancellationUseCase : EndPointNodeBase<ICreateCancellationRequestContract, ICreateCancellationResultContract>
    {
        private readonly IBus _bus;
        public CreateCancellationUseCase(IBus bus) => _bus = bus;
        public override async Task<ICreateCancellationResultContract> Ask(ICreateCancellationRequestContract input) => await new CreateCancellationRequest(_bus).GetResponse(input);
    }
}