using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateArticle
{
    public class CreateArticleHandler : 
        IRequestHandler<CreateArticleCommand, ICreateArticleResultContract>
    {
        private readonly IBus _bus;
        public CreateArticleHandler(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<ICreateArticleResultContract> Handle(
            CreateArticleCommand message, 
            CancellationToken cancellationToken)
        {
            var client = _bus.CreateRequestClient<ICreateArticleRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<ICreateArticleSuccessResultContract, ICreateArticleErrorResultContract>(message, cancellationToken);
            
            if (response.Is(out Response<ICreateArticleSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<ICreateArticleErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}