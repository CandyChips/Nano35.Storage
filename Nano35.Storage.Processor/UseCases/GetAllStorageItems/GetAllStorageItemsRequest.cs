using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllStorageItems
{
    public class GetAllStorageItemsRequest :
        EndPointNodeBase<IGetAllStorageItemsRequestContract, IGetAllStorageItemsResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllStorageItemsRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        public override async Task<IGetAllStorageItemsResultContract> Ask
            (IGetAllStorageItemsRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = await _context
                .StorageItems
                .Where(c => c.InstanceId == input.InstanceId)
                .Select(a => 
                    new StorageItemViewModel()
                    {
                        Id = a.Id,
                        Article = a.Article.ToString(),
                        Comment = a.Comment,
                        Condition = a.Condition.Name,
                        HiddenComment = a.HiddenComment,
                        PurchasePrice = a.PurchasePrice,
                        RetailPrice = a.RetailPrice
                    })
                .ToListAsync(cancellationToken: cancellationToken);

            return new GetAllStorageItemsSuccessResultContract() {Data = result};
        }
    }   
}