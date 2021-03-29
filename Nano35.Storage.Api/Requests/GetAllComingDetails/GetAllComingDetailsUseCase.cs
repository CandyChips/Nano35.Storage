using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllComingDetails
{
    public class GetAllComingDetailsUseCase :
        EndPointNodeBase<IGetAllComingDetailsRequestContract, IGetAllComingDetailsResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllComingDetailsUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllComingDetailsResultContract> Ask(
            IGetAllComingDetailsRequestContract input) => 
            (await (new GetAllComingDetailsRequest(_bus)).GetResponse(input));
    }
}