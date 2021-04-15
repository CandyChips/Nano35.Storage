using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Projection.UseCases.GetAllStorageItemConditions
{
    public class GetAllStorageItemConditionsRequest :
        MasstransitRequest<
            IGetAllStorageItemConditionsRequestContract,
            IGetAllStorageItemConditionsResultContract,
            IGetAllStorageItemConditionsSuccessResultContract,
            IGetAllStorageItemConditionsErrorResultContract>
    {
        public GetAllStorageItemConditionsRequest(IBus bus) : base(bus) {}
    }
}