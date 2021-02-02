using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Api.Requests.Behaviours;

namespace Nano35.Storage.Api.Requests
{
    public class GetAllArticlesCategoryGroupsQuery : 
        IGetAllArticlesCategoryGroupsRequestContract, 
        IQueryRequest<IGetAllArticlesCategoryGroupsResultContract>
    {
        public Guid InstanceId { get; set; }
        
        public class GetAllArticlesCategoryGroupsHandler 
            : IRequestHandler<GetAllArticlesCategoryGroupsQuery, IGetAllArticlesCategoryGroupsResultContract>
        {
            private readonly IBus _bus;
            public GetAllArticlesCategoryGroupsHandler(IBus bus)
            {
                _bus = bus;
            }

            public async Task<IGetAllArticlesCategoryGroupsResultContract> Handle(
                GetAllArticlesCategoryGroupsQuery message,
                CancellationToken cancellationToken)
            {
                var client = _bus.CreateRequestClient<IGetAllArticlesCategoryGroupsRequestContract>(TimeSpan.FromSeconds(10));
                
                var response = await client
                    .GetResponse<IGetAllArticlesCategoryGroupsSuccessResultContract, IGetAllArticlesCategoryGroupsErrorResultContract>(message, cancellationToken);

                if (response.Is(out Response<IGetAllArticlesCategoryGroupsSuccessResultContract> successResponse))
                    return successResponse.Message;
                
                if (response.Is(out Response<IGetAllArticlesCategoryGroupsErrorResultContract> errorResponse))
                    return errorResponse.Message;
                
                throw new InvalidOperationException();
            }
        }
    }
}