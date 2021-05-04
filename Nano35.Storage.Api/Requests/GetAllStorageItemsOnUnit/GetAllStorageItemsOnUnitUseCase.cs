using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllStorageItemsOnUnit
{
    public class GetAllStorageItemsOnUnitUseCase :
        UseCaseEndPointNodeBase<IGetAllStorageItemsOnUnitContract, IGetAllStorageItemsOnUnitResultContract>
    {
        private readonly IBus _bus;
        public GetAllStorageItemsOnUnitUseCase(IBus bus) { _bus = bus; }
        public override async Task<UseCaseResponse<IGetAllStorageItemsOnUnitResultContract>> Ask(IGetAllStorageItemsOnUnitContract input) => 
            await new MasstransitUseCaseRequest<IGetAllStorageItemsOnUnitContract, IGetAllStorageItemsOnUnitResultContract>(_bus, input)
                .GetResponse();
    }
}