using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateArticleModel
{
    public class UpdateArticleModelRequest :
        IPipelineNode<
            IUpdateArticleModelRequestContract, 
            IUpdateArticleModelResultContract>
    {
        private readonly IBus _bus;
        public UpdateArticleModelRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IUpdateArticleModelResultContract> Ask(
            IUpdateArticleModelRequestContract input)
        {
            var client = _bus.CreateRequestClient<IUpdateArticleModelRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IUpdateArticleModelSuccessResultContract, IUpdateArticleModelErrorResultContract>(input);
            
            if (response.Is(out Response<IUpdateArticleModelSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IUpdateArticleModelErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}