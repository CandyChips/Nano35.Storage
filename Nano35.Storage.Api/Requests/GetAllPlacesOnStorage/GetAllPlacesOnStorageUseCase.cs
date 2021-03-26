using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllPlacesOnStorage
{
    public class GetAllPlacesOnStorageUseCase :
        EndPointNodeBase<IGetAllPlacesOnStorageContract, IGetAllPlacesOnStorageResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllPlacesOnStorageUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllPlacesOnStorageResultContract> Ask(
            IGetAllPlacesOnStorageContract input) => 
            (await (new GetAllPlacesOnStorageRequest(_bus)).GetResponse(input));
    }
}