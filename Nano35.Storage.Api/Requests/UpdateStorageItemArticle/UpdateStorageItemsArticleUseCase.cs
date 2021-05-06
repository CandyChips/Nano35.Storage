using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemArticle
{
    public class UpdateStorageItemArticleUseCase :
        UseCaseEndPointNodeBase<IUpdateStorageItemArticleRequestContract, IUpdateStorageItemArticleResultContract>
    {
        private readonly IBus _bus;
        
        public UpdateStorageItemArticleUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<UseCaseResponse<IUpdateStorageItemArticleResultContract>> Ask(IUpdateStorageItemArticleRequestContract input) => 
            await new MasstransitUseCaseRequest<IUpdateStorageItemArticleRequestContract, IUpdateStorageItemArticleResultContract>(_bus, input)
                .GetResponse();
    }
}