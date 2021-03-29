using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllWarehouseNames
{
    public class GetAllWarehouseNamesUseCase :
        EndPointNodeBase<
            IGetAllWarehouseNamesRequestContract,
            IGetAllWarehouseNamesResultContract>
    {
        private readonly IBus _bus;
        public GetAllWarehouseNamesUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllWarehouseNamesResultContract> Ask(
            IGetAllWarehouseNamesRequestContract input) => 
            (await (new GetAllWarehouseNamesRequest(_bus)).GetResponse(input));
    }
}