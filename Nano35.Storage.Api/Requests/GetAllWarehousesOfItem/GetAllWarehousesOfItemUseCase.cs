using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllWarehousesOfItem
{
    public class GetAllWarehousesOfItemUseCase :
        IPipelineNode<
            IGetAllWarehousesOfItemRequestContract,
            IGetAllWarehousesOfItemResultContract>
    {
        private readonly IBus _bus;
        public GetAllWarehousesOfItemUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetAllWarehousesOfItemResultContract> Ask(
            IGetAllWarehousesOfItemRequestContract input) => 
            (await (new GetAllWarehousesOfItemRequest(_bus)).GetResponse(input));
    }
}