using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CreateStorageItem
{
    public class CreateStorageItemRequest :
        UseCaseEndPointNodeBase<ICreateStorageItemRequestContract, ICreateStorageItemResultContract>
    {
        private readonly IBus _bus;
        private readonly ApplicationContext _context;
        public CreateStorageItemRequest(ApplicationContext context, IBus bus) { _context = context; _bus = bus; }
        public override async Task<UseCaseResponse<ICreateStorageItemResultContract>> Ask(
            ICreateStorageItemRequestContract input,
            CancellationToken cancellationToken)
        {
            var item = new StorageItem()
                {Id = input.NewId,
                 Comment = input.Comment,
                 ArticleId = input.ArticleId,
                 ConditionId = input.ConditionId,
                 HiddenComment = input.HiddenComment,
                 InstanceId = input.InstanceId,
                 IsDeleted = false,
                 PurchasePrice = input.PurchasePrice,
                 RetailPrice = input.RetailPrice};
            await _context.StorageItems.AddAsync(item, cancellationToken);
            return Pass(new CreateStorageItemResultContract());
        }
    }
}