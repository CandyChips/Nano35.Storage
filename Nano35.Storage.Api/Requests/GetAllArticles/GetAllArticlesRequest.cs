using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllArticles
{
    public class GetAllArticlesRequest :
        EndPointNodeBase<IGetAllArticlesRequestContract, IGetAllArticlesResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllArticlesRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllArticlesResultContract> Ask(
            IGetAllArticlesRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllArticlesRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetAllArticlesSuccessResultContract, IGetAllArticlesErrorResultContract>(input);
            
            if (response.Is(out Response<IGetAllArticlesSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllArticlesErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}