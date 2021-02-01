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
    public class GetAllStorageItemsQuery : 
        IGetAllStorageItemsRequestContract, 
        IQueryRequest<IGetAllStorageItemsResultContract>
    {
        public Guid InstanceId { get; set; }
        
        public class GetAllStorageItemsHandler 
            : IRequestHandler<GetAllStorageItemsQuery, IGetAllStorageItemsResultContract>
        {
            private readonly IBus _bus;
            public GetAllStorageItemsHandler(IBus bus)
            {
                _bus = bus;
            }

            public async Task<IGetAllStorageItemsResultContract> Handle(
                GetAllStorageItemsQuery message,
                CancellationToken cancellationToken)
            {
                var client = _bus.CreateRequestClient<IGetAllStorageItemsRequestContract>(TimeSpan.FromSeconds(10));
                
                var response = await client
                    .GetResponse<IGetAllStorageItemsSuccessResultContract, IGetAllStorageItemsErrorResultContract>(message, cancellationToken);

                if (response.Is(out Response<IGetAllStorageItemsSuccessResultContract> successResponse))
                    return successResponse.Message;
                
                if (response.Is(out Response<IGetAllStorageItemsErrorResultContract> errorResponse))
                    return errorResponse.Message;
                
                throw new InvalidOperationException();
            }
        }
    }
}