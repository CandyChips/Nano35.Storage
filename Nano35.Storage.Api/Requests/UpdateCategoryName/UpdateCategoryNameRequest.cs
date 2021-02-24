using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateCategoryName
{
    public class UpdateCategoryNameRequest :
        IPipelineNode<
            IUpdateCategoryNameRequestContract, 
            IUpdateCategoryNameResultContract>
    {
        private readonly IBus _bus;
        public UpdateCategoryNameRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IUpdateCategoryNameResultContract> Ask(
            IUpdateCategoryNameRequestContract input)
        {
            var client = _bus.CreateRequestClient<IUpdateCategoryNameRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IUpdateCategoryNameSuccessResultContract, IUpdateCategoryNameErrorResultContract>(input);
            
            if (response.Is(out Response<IUpdateCategoryNameSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IUpdateCategoryNameErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}