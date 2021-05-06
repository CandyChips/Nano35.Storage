using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemPurchasePrice
{
    public class UpdateStorageItemPurchasePriceUseCase :
        UseCaseEndPointNodeBase<IUpdateStorageItemPurchasePriceRequestContract, IUpdateStorageItemPurchasePriceResultContract>
    {
        private readonly IBus _bus;
        
        public UpdateStorageItemPurchasePriceUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<UseCaseResponse<IUpdateStorageItemPurchasePriceResultContract>> Ask(IUpdateStorageItemPurchasePriceRequestContract input) => 
            await new MasstransitUseCaseRequest<IUpdateStorageItemPurchasePriceRequestContract, IUpdateStorageItemPurchasePriceResultContract>(_bus, input)
                .GetResponse();
        
    }
}