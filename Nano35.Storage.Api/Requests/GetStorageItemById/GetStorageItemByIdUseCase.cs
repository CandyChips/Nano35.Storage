using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetStorageItemById
{
    public class GetStorageItemByIdUseCase :
        EndPointNodeBase<IGetStorageItemByIdRequestContract, IGetStorageItemByIdResultContract>
    {
        private readonly IBus _bus;
        
        public GetStorageItemByIdUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetStorageItemByIdResultContract> Ask(
            IGetStorageItemByIdRequestContract input) => 
            (await (new GetStorageItemByIdRequest(_bus)).GetResponse(input));
    }
}