using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemHiddenComment
{
    public class UpdateStorageItemHiddenCommentRequest :
        IPipelineNode<
            IUpdateStorageItemHiddenCommentRequestContract, 
            IUpdateStorageItemHiddenCommentResultContract>
    {
        private readonly IBus _bus;
        public UpdateStorageItemHiddenCommentRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IUpdateStorageItemHiddenCommentResultContract> Ask(
            IUpdateStorageItemHiddenCommentRequestContract input)
        {
            var client = _bus.CreateRequestClient<IUpdateStorageItemHiddenCommentRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IUpdateStorageItemHiddenCommentSuccessResultContract, IUpdateStorageItemHiddenCommentErrorResultContract>(input);
            
            if (response.Is(out Response<IUpdateStorageItemHiddenCommentSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IUpdateStorageItemHiddenCommentErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}