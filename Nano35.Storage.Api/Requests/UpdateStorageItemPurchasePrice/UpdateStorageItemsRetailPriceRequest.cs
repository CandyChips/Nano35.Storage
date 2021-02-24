using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemPurchasePrice
{
    public class UpdateStorageItemPurchasePriceRequest :
        IPipelineNode<
            IUpdateStorageItemPurchasePriceRequestContract, 
            IUpdateStorageItemPurchasePriceResultContract>
    {
        private readonly IBus _bus;
        public UpdateStorageItemPurchasePriceRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IUpdateStorageItemPurchasePriceResultContract> Ask(
            IUpdateStorageItemPurchasePriceRequestContract input)
        {
            var client = _bus.CreateRequestClient<IUpdateStorageItemPurchasePriceRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IUpdateStorageItemPurchasePriceSuccessResultContract, IUpdateStorageItemPurchasePriceErrorResultContract>(input);
            
            if (response.Is(out Response<IUpdateStorageItemPurchasePriceSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IUpdateStorageItemPurchasePriceErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}