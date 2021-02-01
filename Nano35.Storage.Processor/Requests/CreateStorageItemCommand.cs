using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Nano35.Contracts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests
{
    public class CreateStorageItemCommand :
        ICreateStorageItemRequestContract, 
        ICommandRequest<ICreateStorageItemResultContract>
    {
        public Guid NewId { get; set; }
        public Guid ArticleId { get; set; }
        public Guid ConditionId { get; set; }
        public Guid InstanceId { get; set; }
        public string Comment { get; set; }
        public string HiddenComment { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal PurchasePrice { get; set; }

        private class CreateStorageItemSuccessResultContract : 
            ICreateStorageItemSuccessResultContract
        {
            
        }

        private class CreateStorageItemErrorResultContract :
            ICreateStorageItemErrorResultContract
        {
            public string Message { get; set; }
        }

        public class CreateStorageItemHandler : 
            IRequestHandler<CreateStorageItemCommand, ICreateStorageItemResultContract>
        {
            private readonly ApplicationContext _context;
            private readonly IBus _bus;
            
            public CreateStorageItemHandler(
                ApplicationContext context, 
                IBus bus)
            {
                _context = context;
                _bus = bus;
            }
        
            public async Task<ICreateStorageItemResultContract> Handle(
                CreateStorageItemCommand message, 
                CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}