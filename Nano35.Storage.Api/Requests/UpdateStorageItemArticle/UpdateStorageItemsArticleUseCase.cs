using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemArticle
{
    public class UpdateStorageItemArticleUseCase :
        EndPointNodeBase<IUpdateStorageItemArticleRequestContract, IUpdateStorageItemArticleResultContract>
    {
        private readonly IBus _bus;
        
        public UpdateStorageItemArticleUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IUpdateStorageItemArticleResultContract> Ask
            (IUpdateStorageItemArticleRequestContract input) => 
            (await (new UpdateStorageItemArticleRequest(_bus)).GetResponse(input));
    }
}