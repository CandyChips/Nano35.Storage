using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllPlacesOfStorageItemOnInstance
{
    public class GetAllPlacesOfStorageItemOnInstanceUseCase :
        UseCaseEndPointNodeBase<IGetAllPlacesOfStorageItemOnInstanceContract, IGetAllPlacesOfStorageItemOnInstanceResultContract>
    {
        private readonly IBus _bus;
        public GetAllPlacesOfStorageItemOnInstanceUseCase(IBus bus) { _bus = bus; }
        public override async Task<UseCaseResponse<IGetAllPlacesOfStorageItemOnInstanceResultContract>> Ask(IGetAllPlacesOfStorageItemOnInstanceContract input) => 
            await new MasstransitUseCaseRequest<IGetAllPlacesOfStorageItemOnInstanceContract, IGetAllPlacesOfStorageItemOnInstanceResultContract>(_bus, input)
                .GetResponse();
    }
}