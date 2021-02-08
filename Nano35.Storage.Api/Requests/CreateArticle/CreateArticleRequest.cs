using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateArticle
{
    public class CreateArticleRequest :
        IPipelineNode<ICreateArticleRequestContract, ICreateArticleResultContract>
    {
        private readonly IBus _bus;
        public CreateArticleRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        private class CreateArticleSuccessResultContract : 
            ICreateArticleSuccessResultContract
        {
            
        }
        
        public async Task<ICreateArticleResultContract> Ask(
            ICreateArticleRequestContract input)
        {
            var client = _bus.CreateRequestClient<ICreateArticleRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<ICreateArticleSuccessResultContract, ICreateArticleErrorResultContract>(input);
            
            if (response.Is(out Response<ICreateArticleSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<ICreateArticleErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}