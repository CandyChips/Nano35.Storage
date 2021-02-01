using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Api.Requests.Behaviours;

namespace Nano35.Storage.Api.Requests
{
    public class GetAllArticlesBrandsQuery : 
        IGetAllArticlesBrandsRequestContract, 
        IQueryRequest<IGetAllArticlesBrandsResultContract>
    {
        public Guid InstanceId { get; set; }
        
        public class GetAllArticlesBrandsHandler 
            : IRequestHandler<GetAllArticlesBrandsQuery, IGetAllArticlesBrandsResultContract>
        {
            private readonly IBus _bus;
            public GetAllArticlesBrandsHandler(IBus bus)
            {
                _bus = bus;
            }

            public async Task<IGetAllArticlesBrandsResultContract> Handle(
                GetAllArticlesBrandsQuery message,
                CancellationToken cancellationToken)
            {
                var client = _bus.CreateRequestClient<IGetAllArticlesBrandsRequestContract>(TimeSpan.FromSeconds(10));
                
                var response = await client
                    .GetResponse<IGetAllArticlesBrandsSuccessResultContract, IGetAllArticlesBrandsErrorResultContract>(message, cancellationToken);

                if (response.Is(out Response<IGetAllArticlesBrandsSuccessResultContract> successResponse))
                    return successResponse.Message;
                
                if (response.Is(out Response<IGetAllArticlesBrandsErrorResultContract> errorResponse))
                    return errorResponse.Message;
                
                throw new InvalidOperationException();
            }
        }
    }
}