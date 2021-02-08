using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateStorageItem
{
    public class CreateStorageItemRequest :
        IPipelineNode<ICreateStorageItemRequestContract, ICreateStorageItemResultContract>
    {
        private readonly IBus _bus;
        public CreateStorageItemRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        private class CreateStorageItemSuccessResultContract : 
            ICreateStorageItemSuccessResultContract
        {
            
        }
        
        public async Task<ICreateStorageItemResultContract> Ask(
            ICreateStorageItemRequestContract input)
        {
            var client = _bus.CreateRequestClient<ICreateStorageItemRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<ICreateStorageItemSuccessResultContract, ICreateStorageItemErrorResultContract>(input);
            
            if (response.Is(out Response<ICreateStorageItemSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<ICreateStorageItemErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}