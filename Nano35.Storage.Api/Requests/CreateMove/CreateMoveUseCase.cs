using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateMove
{
    public class CreateMoveUseCase : EndPointNodeBase<ICreateMoveRequestContract, ICreateMoveResultContract>
    {
        private readonly IBus _bus;
        public CreateMoveUseCase(IBus bus) => _bus = bus;
        public override async Task<ICreateMoveResultContract> Ask(ICreateMoveRequestContract input) => await new CreateMoveRequest(_bus).GetResponse(input);
    }
}