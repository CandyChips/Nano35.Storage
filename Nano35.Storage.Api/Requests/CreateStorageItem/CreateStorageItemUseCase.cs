using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateStorageItem
{
    public class CreateStorageItemUseCase :
        UseCaseEndPointNodeBase<ICreateStorageItemRequestContract, ICreateStorageItemResultContract>
    {
        private readonly IBus _bus;
        public CreateStorageItemUseCase(IBus bus) { _bus = bus; }
        
        public override async Task<UseCaseResponse<ICreateStorageItemResultContract>> Ask(ICreateStorageItemRequestContract input) => 
            await new MasstransitUseCaseRequest<ICreateStorageItemRequestContract, ICreateStorageItemResultContract>(_bus, input)
                .GetResponse();
    }
}