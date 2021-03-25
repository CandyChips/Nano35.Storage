using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemComment
{
    public class UpdateStorageItemCommentRequest :
        EndPointNodeBase<IUpdateStorageItemCommentRequestContract, IUpdateStorageItemCommentResultContract>
    {
        private readonly IBus _bus;
        
        public UpdateStorageItemCommentRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IUpdateStorageItemCommentResultContract> Ask(
            IUpdateStorageItemCommentRequestContract input)
        {
            var client = _bus.CreateRequestClient<IUpdateStorageItemCommentRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IUpdateStorageItemCommentSuccessResultContract, IUpdateStorageItemCommentErrorResultContract>(input);
            
            if (response.Is(out Response<IUpdateStorageItemCommentSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IUpdateStorageItemCommentErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}