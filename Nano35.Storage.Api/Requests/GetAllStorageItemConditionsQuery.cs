using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Api.Requests.Behaviours;

namespace Nano35.Storage.Api.Requests
{
    public class GetAllStorageItemConditionsQuery : 
        IGetAllStorageItemConditionsRequestContract, 
        IQueryRequest<IGetAllStorageItemConditionsResultContract>
    {
        public class GetAllStorageItemConditionsHandler 
            : IRequestHandler<GetAllStorageItemConditionsQuery, IGetAllStorageItemConditionsResultContract>
        {
            private readonly IBus _bus;
            public GetAllStorageItemConditionsHandler(IBus bus)
            {
                _bus = bus;
            }

            public async Task<IGetAllStorageItemConditionsResultContract> Handle(
                GetAllStorageItemConditionsQuery message,
                CancellationToken cancellationToken)
            {
                var client = _bus.CreateRequestClient<IGetAllStorageItemConditionsRequestContract>(TimeSpan.FromSeconds(10));
                
                var response = await client
                    .GetResponse<IGetAllStorageItemConditionsSuccessResultContract, IGetAllStorageItemConditionsErrorResultContract>(message, cancellationToken);

                if (response.Is(out Response<IGetAllStorageItemConditionsSuccessResultContract> successResponse))
                    return successResponse.Message;
                
                if (response.Is(out Response<IGetAllStorageItemConditionsErrorResultContract> errorResponse))
                    return errorResponse.Message;
                
                throw new InvalidOperationException();
            }
        }

    }
}