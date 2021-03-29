using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemHiddenComment
{
    public class UpdateStorageItemHiddenCommentUseCase :
        EndPointNodeBase<IUpdateStorageItemHiddenCommentRequestContract, IUpdateStorageItemHiddenCommentResultContract>
    {
        private readonly IBus _bus;
        
        public UpdateStorageItemHiddenCommentUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IUpdateStorageItemHiddenCommentResultContract> Ask
            (IUpdateStorageItemHiddenCommentRequestContract input) => 
            (await (new UpdateStorageItemHiddenCommentRequest(_bus)).GetResponse(input));
    }
}