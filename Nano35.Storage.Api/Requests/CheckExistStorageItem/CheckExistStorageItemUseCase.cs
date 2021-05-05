using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CheckExistStorageItem
{
    public class CheckExistStorageItemUseCase : UseCaseEndPointNodeBase<ICheckExistStorageItemRequestContract, ICheckExistStorageItemResultContract>
    {
        private readonly IBus _bus;
        public CheckExistStorageItemUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<ICheckExistStorageItemResultContract>> Ask(ICheckExistStorageItemRequestContract input) => 
            await new MasstransitUseCaseRequest<ICheckExistStorageItemRequestContract, ICheckExistStorageItemResultContract>(_bus, input)
                .GetResponse();
    }
}