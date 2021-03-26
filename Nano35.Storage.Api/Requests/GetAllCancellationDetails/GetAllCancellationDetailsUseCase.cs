using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllCancellationDetails
{
    public class GetAllCancellationDetailsUseCase :
        EndPointNodeBase<IGetAllCancellationDetailsRequestContract, IGetAllCancellationDetailsResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllCancellationDetailsUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllCancellationDetailsResultContract> Ask(
            IGetAllCancellationDetailsRequestContract input) => 
            (await (new GetAllCancellationDetailsRequest(_bus)).GetResponse(input));
    }
}