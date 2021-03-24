using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetArticleById
{
    public class GetArticleByIdRequest :
        EndPointNodeBase<IGetArticleByIdRequestContract, IGetArticleByIdResultContract>
    {
        private readonly IBus _bus;
        
        public GetArticleByIdRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetArticleByIdResultContract> Ask(
            IGetArticleByIdRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetArticleByIdRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetArticleByIdSuccessResultContract, IGetArticleByIdErrorResultContract>(input);
            
            if (response.Is(out Response<IGetArticleByIdSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetArticleByIdErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}