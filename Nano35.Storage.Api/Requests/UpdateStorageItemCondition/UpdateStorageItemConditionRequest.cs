using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemCondition
{
    public class UpdateStorageItemConditionRequest :
        EndPointNodeBase<IUpdateStorageItemConditionRequestContract, IUpdateStorageItemConditionResultContract>
    {
        private readonly IBus _bus;
        
        public UpdateStorageItemConditionRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IUpdateStorageItemConditionResultContract> Ask(
            IUpdateStorageItemConditionRequestContract input)
        {
            var client = _bus.CreateRequestClient<IUpdateStorageItemConditionRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IUpdateStorageItemConditionSuccessResultContract, IUpdateStorageItemConditionErrorResultContract>(input);
            
            if (response.Is(out Response<IUpdateStorageItemConditionSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IUpdateStorageItemConditionErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}