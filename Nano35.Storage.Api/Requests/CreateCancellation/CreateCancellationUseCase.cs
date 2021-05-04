using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateCancellation
{
    public class CreateCancellationUseCase : UseCaseEndPointNodeBase<ICreateCancellationRequestContract, ICreateCancellationResultContract>
    {
        private readonly IBus _bus;
        public CreateCancellationUseCase(IBus bus) => _bus = bus;

        public override async Task<UseCaseResponse<ICreateCancellationResultContract>> Ask(ICreateCancellationRequestContract input) => 
            await new MasstransitUseCaseRequest<ICreateCancellationRequestContract, ICreateCancellationResultContract>(_bus, input)
                .GetResponse();    }
}