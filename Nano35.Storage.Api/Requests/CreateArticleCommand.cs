using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Api.Requests.Behaviours;

namespace Nano35.Storage.Api.Requests
{
    public class CreateArticleCommand :
        ICreateArticleRequestContract, 
        ICommandRequest<ICreateArticleResultContract>
    {
        public Guid NewId { get; set; }
        public Guid InstanceId { get; set; }
        public Guid ArticleTypeId { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string CategoryGroup { get; set; }
    
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
}