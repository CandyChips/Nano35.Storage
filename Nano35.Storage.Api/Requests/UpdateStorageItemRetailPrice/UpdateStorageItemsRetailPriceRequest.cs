using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemRetailPrice
{
    public class UpdateStorageItemRetailPriceRequest :
        IPipelineNode<
            IUpdateStorageItemRetailPriceRequestContract, 
            IUpdateStorageItemRetailPriceResultContract>
    {
        private readonly IBus _bus;
        public UpdateStorageItemRetailPriceRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IUpdateStorageItemRetailPriceResultContract> Ask(
            IUpdateStorageItemRetailPriceRequestContract input)
        {
            var client = _bus.CreateRequestClient<IUpdateStorageItemRetailPriceRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IUpdateStorageItemRetailPriceSuccessResultContract, IUpdateStorageItemRetailPriceErrorResultContract>(input);
            
            if (response.Is(out Response<IUpdateStorageItemRetailPriceSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IUpdateStorageItemRetailPriceErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}