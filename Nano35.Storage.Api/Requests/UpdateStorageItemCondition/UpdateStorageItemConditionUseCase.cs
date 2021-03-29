using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemCondition
{
    public class UpdateStorageItemConditionUseCase :
        EndPointNodeBase<IUpdateStorageItemConditionRequestContract, IUpdateStorageItemConditionResultContract>
    {
        private readonly IBus _bus;
        
        public UpdateStorageItemConditionUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IUpdateStorageItemConditionResultContract> Ask
            (IUpdateStorageItemConditionRequestContract input) => 
            (await (new UpdateStorageItemConditionRequest(_bus)).GetResponse(input));
    }
}