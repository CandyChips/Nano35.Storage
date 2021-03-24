using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllPlacesOnStorage
{
    public class GetAllPlacesOnStorageRequest :
        EndPointNodeBase<IGetAllPlacesOnStorageContract, IGetAllPlacesOnStorageResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllPlacesOnStorageRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllPlacesOnStorageResultContract> Ask(
            IGetAllPlacesOnStorageContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllPlacesOnStorageContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetAllPlacesOnStorageSuccessResultContract, IGetAllPlacesOnStorageErrorResultContract>(input);
            
            if (response.Is(out Response<IGetAllPlacesOnStorageSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllPlacesOnStorageErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}