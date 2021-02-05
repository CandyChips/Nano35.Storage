using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateStorageItem
{
    public class CreateStorageItemHandler : 
        IRequestHandler<CreateStorageItemCommand, ICreateStorageItemResultContract>
    {
        private readonly IBus _bus;
        public CreateStorageItemHandler(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<ICreateStorageItemResultContract> Handle(
            CreateStorageItemCommand message, 
            CancellationToken cancellationToken)
        {
            var client = _bus.CreateRequestClient<ICreateStorageItemRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<ICreateStorageItemSuccessResultContract, ICreateStorageItemErrorResultContract>(message, cancellationToken);
            
            if (response.Is(out Response<ICreateStorageItemSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<ICreateStorageItemErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}