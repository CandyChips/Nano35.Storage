using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllPlacesOfStorageItemOnUnit
{
    public class GetAllPlacesOfStorageItemOnUnitUseCase :
        EndPointNodeBase<
            IGetAllPlacesOfStorageItemOnUnitRequestContract,
            IGetAllPlacesOfStorageItemOnUnitResultContract>
    {
        private readonly IBus _bus;
        public GetAllPlacesOfStorageItemOnUnitUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllPlacesOfStorageItemOnUnitResultContract> Ask(
            IGetAllPlacesOfStorageItemOnUnitRequestContract input) => 
            (await (new GetAllPlacesOfStorageItemOnUnitRequest(_bus)).GetResponse(input));
    }
}