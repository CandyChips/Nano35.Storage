using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllWarehousesOfItem
{
    public class GetAllWarehousesOfItemRequest :
        IPipelineNode<
            IGetAllWarehousesOfItemRequestContract,
            IGetAllWarehousesOfItemResultContract>
    {
        private readonly IBus _bus;
        public GetAllWarehousesOfItemRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetAllWarehousesOfItemResultContract> Ask(
            IGetAllWarehousesOfItemRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllWarehousesOfItemRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetAllWarehousesOfItemSuccessResultContract, IGetAllWarehousesOfItemErrorResultContract>(input);
            
            if (response.Is(out Response<IGetAllWarehousesOfItemSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllWarehousesOfItemErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}