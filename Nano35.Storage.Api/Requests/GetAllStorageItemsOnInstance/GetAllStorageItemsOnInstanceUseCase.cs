using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllStorageItemsOnInstance
{
    public class GetAllStorageItemsOnInstanceUseCase :
        EndPointNodeBase<
            IGetAllStorageItemsOnInstanceContract,
            IGetAllStorageItemsOnInstanceResultContract>
    {
        private readonly IBus _bus;
        public GetAllStorageItemsOnInstanceUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllStorageItemsOnInstanceResultContract> Ask(
            IGetAllStorageItemsOnInstanceContract input) => 
            (await (new GetAllStorageItemsOnInstanceRequest(_bus)).GetResponse(input));
    }
}