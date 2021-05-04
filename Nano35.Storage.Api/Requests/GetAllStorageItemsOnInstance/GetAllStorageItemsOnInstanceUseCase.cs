using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllStorageItemsOnInstance
{
    public class GetAllStorageItemsOnInstanceUseCase :
        UseCaseEndPointNodeBase<IGetAllStorageItemsOnInstanceContract, IGetAllStorageItemsOnInstanceResultContract>
    {
        private readonly IBus _bus;
        public GetAllStorageItemsOnInstanceUseCase(IBus bus) { _bus = bus; }
        public override async Task<UseCaseResponse<IGetAllStorageItemsOnInstanceResultContract>> Ask(IGetAllStorageItemsOnInstanceContract input) => 
            await new MasstransitUseCaseRequest<IGetAllStorageItemsOnInstanceContract, IGetAllStorageItemsOnInstanceResultContract>(_bus, input)
                .GetResponse();
    }
}