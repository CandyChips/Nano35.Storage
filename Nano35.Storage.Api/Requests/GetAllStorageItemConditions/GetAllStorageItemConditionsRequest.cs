using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllStorageItemConditions
{
    public class GetAllStorageItemConditionsRequest :
        IPipelineNode<
            IGetAllStorageItemConditionsRequestContract, 
            IGetAllStorageItemConditionsResultContract>
    {
        private readonly IBus _bus;
        public GetAllStorageItemConditionsRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetAllStorageItemConditionsResultContract> Ask(
            IGetAllStorageItemConditionsRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllStorageItemConditionsRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetAllStorageItemConditionsSuccessResultContract, IGetAllStorageItemConditionsErrorResultContract>(input);
            
            if (response.Is(out Response<IGetAllStorageItemConditionsSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllStorageItemConditionsErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}