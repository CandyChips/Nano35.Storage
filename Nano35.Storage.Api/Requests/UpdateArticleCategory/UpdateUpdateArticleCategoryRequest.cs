using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateArticleCategory
{
    public class UpdateArticleCategoryRequest :
        IPipelineNode<
            IUpdateArticleCategoryRequestContract, 
            IUpdateArticleCategoryResultContract>
    {
        private readonly IBus _bus;
        public UpdateArticleCategoryRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IUpdateArticleCategoryResultContract> Ask(
            IUpdateArticleCategoryRequestContract input)
        {
            var client = _bus.CreateRequestClient<IUpdateArticleCategoryRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IUpdateArticleCategorySuccessResultContract, IUpdateArticleCategoryErrorResultContract>(input);
            
            if (response.Is(out Response<IUpdateArticleCategorySuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IUpdateArticleCategoryErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}