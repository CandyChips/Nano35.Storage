using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllMoveDetails
{
    public class GetAllMoveDetailsUseCase :
        EndPointNodeBase<IGetAllMoveDetailsRequestContract, IGetAllMoveDetailsResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllMoveDetailsUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllMoveDetailsResultContract> Ask(
            IGetAllMoveDetailsRequestContract input) => 
            (await (new GetAllMoveDetailsRequest(_bus)).GetResponse(input));
    }
}