using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemCondition
{
    public class UpdateStorageItemConditionRequest :
        MasstransitRequest
        <IUpdateStorageItemConditionRequestContract,
            IUpdateStorageItemConditionResultContract,
            IUpdateStorageItemConditionSuccessResultContract,
            IUpdateStorageItemConditionErrorResultContract>
    {
        public UpdateStorageItemConditionRequest(IBus bus) : base(bus)
        {
        }
    }
}