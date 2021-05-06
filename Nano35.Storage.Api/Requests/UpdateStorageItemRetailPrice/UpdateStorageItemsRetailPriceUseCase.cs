using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemRetailPrice
{
    public class UpdateStorageItemRetailPriceUseCase :
        UseCaseEndPointNodeBase<IUpdateStorageItemRetailPriceRequestContract, IUpdateStorageItemRetailPriceResultContract>
    {
        private readonly IBus _bus;
        
        public UpdateStorageItemRetailPriceUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<UseCaseResponse<IUpdateStorageItemRetailPriceResultContract>> Ask(IUpdateStorageItemRetailPriceRequestContract input) => 
            await new MasstransitUseCaseRequest<IUpdateStorageItemRetailPriceRequestContract, IUpdateStorageItemRetailPriceResultContract>(_bus, input)
                .GetResponse();
    }
}