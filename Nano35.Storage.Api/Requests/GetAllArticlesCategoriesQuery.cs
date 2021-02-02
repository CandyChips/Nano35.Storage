using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Api.Requests.Behaviours;

namespace Nano35.Storage.Api.Requests
{
    public class GetAllArticlesCategoriesQuery : 
        IGetAllArticlesCategoriesRequestContract, 
        IQueryRequest<IGetAllArticlesCategoriesResultContract>
    {
        public Guid InstanceId { get; set; }
        
        public class GetAllArticlesCategoriesHandler 
            : IRequestHandler<GetAllArticlesCategoriesQuery, IGetAllArticlesCategoriesResultContract>
        {
            private readonly IBus _bus;
            public GetAllArticlesCategoriesHandler(IBus bus)
            {
                _bus = bus;
            }

            public async Task<IGetAllArticlesCategoriesResultContract> Handle(
                GetAllArticlesCategoriesQuery message,
                CancellationToken cancellationToken)
            {
                var client = _bus.CreateRequestClient<IGetAllArticlesCategoriesRequestContract>(TimeSpan.FromSeconds(10));
                
                var response = await client
                    .GetResponse<IGetAllArticlesCategoriesSuccessResultContract, IGetAllArticlesCategoriesErrorResultContract>(message, cancellationToken);

                if (response.Is(out Response<IGetAllArticlesCategoriesSuccessResultContract> successResponse))
                    return successResponse.Message;
                
                if (response.Is(out Response<IGetAllArticlesCategoriesErrorResultContract> errorResponse))
                    return errorResponse.Message;
                
                throw new InvalidOperationException();
            }
        }
    }
}