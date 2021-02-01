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
    public class CreateStorageItemCommand :
        ICreateStorageItemRequestContract, 
        ICommandRequest<ICreateStorageItemResultContract>
    {
        public Guid NewId { get; set; }
        public Guid InstanceId { get; set; }
        public string Comment { get; set; }
        public string HiddenComment { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public Guid ArticleId { get; set; }
        public Guid ConditionId { get; set; }
    
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
}