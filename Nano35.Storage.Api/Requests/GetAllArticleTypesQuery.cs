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
    public class GetAllArticleTypesQuery : 
        IGetAllArticleTypesRequestContract, 
        IQueryRequest<IGetAllArticleTypesResultContract>
    {
        
        public class GetAllArticlesBrandsHandler 
            : IRequestHandler<GetAllArticleTypesQuery, IGetAllArticleTypesResultContract>
        {
            private readonly IBus _bus;
            public GetAllArticlesBrandsHandler(IBus bus)
            {
                _bus = bus;
            }

            public async Task<IGetAllArticleTypesResultContract> Handle(
                GetAllArticleTypesQuery message,
                CancellationToken cancellationToken)
            {
                var client = _bus.CreateRequestClient<IGetAllArticleTypesRequestContract>(TimeSpan.FromSeconds(10));
                
                var response = await client
                    .GetResponse<IGetAllArticleTypesSuccessResultContract, IGetAllArticleTypesErrorResultContract>(message, cancellationToken);

                if (response.Is(out Response<IGetAllArticleTypesSuccessResultContract> successResponse))
                    return successResponse.Message;
                
                if (response.Is(out Response<IGetAllArticleTypesErrorResultContract> errorResponse))
                    return errorResponse.Message;
                
                throw new InvalidOperationException();
            }
        }
    }
}