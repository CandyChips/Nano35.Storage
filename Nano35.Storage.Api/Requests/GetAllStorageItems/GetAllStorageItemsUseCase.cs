using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllStorageItems
{
    public class GetAllStorageItemsUseCase :
        UseCaseEndPointNodeBase<IGetAllStorageItemsRequestContract, IGetAllStorageItemsResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllStorageItemsUseCase(IBus bus) { _bus = bus; }
        public override async Task<UseCaseResponse<IGetAllStorageItemsResultContract>> Ask(IGetAllStorageItemsRequestContract input) => 
            await new MasstransitUseCaseRequest<IGetAllStorageItemsRequestContract, IGetAllStorageItemsResultContract>(_bus, input)
                .GetResponse();
    }
}