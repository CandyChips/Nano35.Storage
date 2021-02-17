using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllStorageItems
{
    public class GetAllStorageItemsRequest :
        IPipelineNode<
            IGetAllStorageItemsRequestContract, 
            IGetAllStorageItemsResultContract>
    {
        private readonly IBus _bus;
        public GetAllStorageItemsRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetAllStorageItemsResultContract> Ask(
            IGetAllStorageItemsRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllStorageItemsRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetAllStorageItemsSuccessResultContract, IGetAllStorageItemsErrorResultContract>(input);

            if (response.Is(out Response<IGetAllStorageItemsSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllStorageItemsErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}