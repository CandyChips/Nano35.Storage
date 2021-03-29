using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllWarehousesOfUnit
{
    public class GetAllWarehousesOfUnitUseCase :
        IPipelineNode<
            IGetAllWarehousesOfUnitRequestContract,
            IGetAllWarehousesOfUnitResultContract>
    {
        private readonly IBus _bus;
        public GetAllWarehousesOfUnitUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetAllWarehousesOfUnitResultContract> Ask(
            IGetAllWarehousesOfUnitRequestContract input) => 
            (await (new GetAllWarehousesOfUnitRequest(_bus)).GetResponse(input));
    }
}