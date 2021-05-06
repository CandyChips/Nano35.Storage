using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemHiddenComment
{
    public class UpdateStorageItemHiddenCommentUseCase :
        UseCaseEndPointNodeBase<IUpdateStorageItemHiddenCommentRequestContract, IUpdateStorageItemHiddenCommentResultContract>
    {
        private readonly IBus _bus;
        
        public UpdateStorageItemHiddenCommentUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<UseCaseResponse<IUpdateStorageItemHiddenCommentResultContract>> Ask(IUpdateStorageItemHiddenCommentRequestContract input) => 
            await new MasstransitUseCaseRequest<IUpdateStorageItemHiddenCommentRequestContract, IUpdateStorageItemHiddenCommentResultContract>(_bus, input)
                .GetResponse();
    }
}