using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemComment
{
    public class UpdateStorageItemCommentUseCase :
        EndPointNodeBase<IUpdateStorageItemCommentRequestContract, IUpdateStorageItemCommentResultContract>
    {
        private readonly IBus _bus;
        
        public UpdateStorageItemCommentUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IUpdateStorageItemCommentResultContract> Ask
            (IUpdateStorageItemCommentRequestContract input) => 
            (await (new UpdateStorageItemCommentRequest(_bus)).GetResponse(input));
    }
}