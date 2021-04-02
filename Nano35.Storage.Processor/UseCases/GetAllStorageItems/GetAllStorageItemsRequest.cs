using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
                .MapAllToAsync<StorageItemViewModel>();

            return new GetAllStorageItemsSuccessResultContract() {Data = result};
        }
    }   
}