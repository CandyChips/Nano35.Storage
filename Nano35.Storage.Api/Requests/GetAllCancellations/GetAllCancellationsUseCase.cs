using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllCancellations
{
    public class GetAllCancellationsUseCase :
        EndPointNodeBase<IGetAllCancellationsRequestContract, IGetAllCancellationsResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllCancellationsUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllCancellationsResultContract> Ask(
            IGetAllCancellationsRequestContract input) => 
            (await (new GetAllCancellationsRequest(_bus)).GetResponse(input));
    }
}