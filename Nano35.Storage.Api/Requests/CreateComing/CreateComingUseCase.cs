using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateComing
{
    public class CreateComingUseCase : UseCaseEndPointNodeBase<ICreateComingRequestContract, ICreateComingResultContract>
    {
        private readonly IBus _bus;
        public CreateComingUseCase(IBus bus) => _bus = bus;

        public override async Task<UseCaseResponse<ICreateComingResultContract>> Ask(ICreateComingRequestContract input) => 
            await new MasstransitUseCaseRequest<ICreateComingRequestContract, ICreateComingResultContract>(_bus, input)
                .GetResponse();    }
}