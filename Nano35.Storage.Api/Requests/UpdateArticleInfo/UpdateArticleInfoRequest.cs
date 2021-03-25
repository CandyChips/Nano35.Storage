using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateArticleInfo
{
    public class UpdateArticleInfoRequest :
        EndPointNodeBase<IUpdateArticleInfoRequestContract, IUpdateArticleInfoResultContract>
    {
        private readonly IBus _bus;
        
        public UpdateArticleInfoRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IUpdateArticleInfoResultContract> Ask(
            IUpdateArticleInfoRequestContract input)
        {
            var client = _bus.CreateRequestClient<IUpdateArticleInfoRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IUpdateArticleInfoSuccessResultContract, IUpdateArticleInfoErrorResultContract>(input);
            
            if (response.Is(out Response<IUpdateArticleInfoSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IUpdateArticleInfoErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}