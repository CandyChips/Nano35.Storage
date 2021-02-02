using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nano35.Contracts;
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
            
            public CreateStorageItemHandler(
                ApplicationContext context)
            {
                _context = context;
            }
        
            public async Task<ICreateStorageItemResultContract> Handle(
                CreateStorageItemCommand message, 
                CancellationToken cancellationToken)
            {
                try
                {
                    var storageItem = new StorageItem()
                    {
                        Id = message.NewId,
                        InstanceId = message.InstanceId,
                        Comment = message.Comment,
                        HiddenComment = message.HiddenComment,
                        RetailPrice = message.RetailPrice,
                        PurchasePrice = message.PurchasePrice,
                        IsDeleted = false,
                        ArticleId = message.ArticleId,
                        ConditionId = message.ConditionId 
                    };
                    await _context.StorageItems.AddAsync(storageItem, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    return new CreateStorageItemSuccessResultContract();
                }
                catch
                {
                    return new CreateStorageItemErrorResultContract();
                }
            }
        }
    }
}