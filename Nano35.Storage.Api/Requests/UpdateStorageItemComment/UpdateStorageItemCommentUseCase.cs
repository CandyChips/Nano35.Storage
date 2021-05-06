using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemComment
{
    public class UpdateStorageItemCommentUseCase :
        UseCaseEndPointNodeBase<IUpdateStorageItemCommentRequestContract, IUpdateStorageItemCommentResultContract>
    {
        private readonly IBus _bus;
        
        public UpdateStorageItemCommentUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<UseCaseResponse<IUpdateStorageItemCommentResultContract>> Ask(IUpdateStorageItemCommentRequestContract input) => 
            await new MasstransitUseCaseRequest<IUpdateStorageItemCommentRequestContract, IUpdateStorageItemCommentResultContract>(_bus, input)
                .GetResponse();
    }
}