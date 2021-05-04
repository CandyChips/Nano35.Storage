using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateMove
{
    public class CreateMoveUseCase : UseCaseEndPointNodeBase<ICreateMoveRequestContract, ICreateMoveResultContract>
    {
        private readonly IBus _bus;
        public CreateMoveUseCase(IBus bus) => _bus = bus;

        public override async Task<UseCaseResponse<ICreateMoveResultContract>> Ask(ICreateMoveRequestContract input) => 
            await new MasstransitUseCaseRequest<ICreateMoveRequestContract, ICreateMoveResultContract>(_bus, input)
                .GetResponse();    }
}