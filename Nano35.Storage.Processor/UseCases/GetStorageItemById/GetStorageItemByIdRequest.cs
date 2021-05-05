using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.files;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetStorageItemById
{
    public class GetStorageItemByIdRequest :
        UseCaseEndPointNodeBase<IGetStorageItemByIdRequestContract, IGetStorageItemByIdResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetStorageItemByIdRequest(ApplicationContext context, IBus bus)
        {
            _context = context;
            _bus = bus;
        }
        
        public override async Task<UseCaseResponse<IGetStorageItemByIdResultContract>> Ask(
            IGetStorageItemByIdRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = await _context.StorageItems
                .FirstOrDefaultAsync(c => c.Id == input.Id, cancellationToken: cancellationToken);

            if (result == null)
                return new UseCaseResponse<IGetStorageItemByIdResultContract>("Не найдено");
            
            return new UseCaseResponse<IGetStorageItemByIdResultContract>(new GetStorageItemByIdResultContract()
            {
                Data =
                    new StorageItemViewModel()
                    {
                        Article = result.Article.ToString(),
                        Comment = result.Comment,
                        Condition = result.Condition.Name,
                        Id = result.Id,
                        HiddenComment = result.HiddenComment,
                        PurchasePrice = result.PurchasePrice,
                        RetailPrice = result.RetailPrice,
                        Images = (new GetImagesOfStorageItem(_bus,
                                new GetImagesOfStorageItemRequestContract() {StorageItemId = result.Id}).GetResponse()
                            .Result as IGetImagesOfStorageItemSuccessResultContract)?.Images
                    }
            });
        }
    }   
}