using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateSelle
{
    public class CreateSelleUseCase :
        EndPointNodeBase<ICreateSelleRequestContract, ICreateSelleResultContract>
    {
        private readonly IBus _bus;
        
        public CreateSelleUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<ICreateSelleResultContract> Ask(
            ICreateSelleRequestContract input) => 
            (await (new CreateSelleRequest(_bus)).GetResponse(input));
    }
}