using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateCategoryParentCategoryId
{
    public class UpdateCategoryParentCategoryIdRequest :
        EndPointNodeBase<IUpdateCategoryParentCategoryIdRequestContract, IUpdateCategoryParentCategoryIdResultContract>
    {
        private readonly IBus _bus;
        public UpdateCategoryParentCategoryIdRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IUpdateCategoryParentCategoryIdResultContract> Ask(
            IUpdateCategoryParentCategoryIdRequestContract input)
        {
            var client = _bus.CreateRequestClient<IUpdateCategoryParentCategoryIdRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IUpdateCategoryParentCategoryIdSuccessResultContract, IUpdateCategoryParentCategoryIdErrorResultContract>(input);
            
            if (response.Is(out Response<IUpdateCategoryParentCategoryIdSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IUpdateCategoryParentCategoryIdErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}