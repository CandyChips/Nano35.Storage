using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemArticle
{
    public class UpdateStorageItemArticleRequest :
        IPipelineNode<
            IUpdateStorageItemArticleRequestContract, 
            IUpdateStorageItemArticleResultContract>
    {
        private readonly IBus _bus;
        public UpdateStorageItemArticleRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IUpdateStorageItemArticleResultContract> Ask(
            IUpdateStorageItemArticleRequestContract input)
        {
            var client = _bus.CreateRequestClient<IUpdateStorageItemArticleRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IUpdateStorageItemArticleSuccessResultContract, IUpdateStorageItemArticleErrorResultContract>(input);
            
            if (response.Is(out Response<IUpdateStorageItemArticleSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IUpdateStorageItemArticleErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}