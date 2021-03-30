using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllPlacesOfStorageItemOnInstance
{
    public class GetAllPlacesOfStorageItemOnInstanceUseCase :
        EndPointNodeBase<
            IGetAllPlacesOfStorageItemOnInstanceContract,
            IGetAllPlacesOfStorageItemOnInstanceResultContract>
    {
        private readonly IBus _bus;
        public GetAllPlacesOfStorageItemOnInstanceUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllPlacesOfStorageItemOnInstanceResultContract> Ask(
            IGetAllPlacesOfStorageItemOnInstanceContract input) => 
            (await (new GetAllPlacesOfStorageItemOnInstanceRequest(_bus)).GetResponse(input));
    }
}