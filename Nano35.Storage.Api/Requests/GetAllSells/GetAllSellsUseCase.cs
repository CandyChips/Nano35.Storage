using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllSells
{
    public class GetAllSellsUseCase :
        EndPointNodeBase<IGetAllSellsRequestContract, IGetAllSellsResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllSellsUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllSellsResultContract> Ask(
            IGetAllSellsRequestContract input) => 
            (await (new GetAllSellsRequest(_bus)).GetResponse(input));
    }
}