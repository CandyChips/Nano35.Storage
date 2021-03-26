using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllSelleDetails
{
    public class GetAllSelleDetailsUseCase :
        EndPointNodeBase<IGetAllSelleDetailsRequestContract, IGetAllSelleDetailsResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllSelleDetailsUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllSelleDetailsResultContract> Ask(
            IGetAllSelleDetailsRequestContract input) => 
            (await (new GetAllSelleDetailsRequest(_bus)).GetResponse(input));
    }
}