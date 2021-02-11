using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.GetAllArticleBrands
{
    public class GetStorageItemByIdRequest :
        IPipelineNode<
            IGetStorageItemByIdRequestContract,
            IGetStorageItemByIdResultContract>
    {
        private readonly ApplicationContext _context;

        public GetStorageItemByIdRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetStorageItemByIdSuccessResultContract : 
            IGetStorageItemByIdSuccessResultContract
        {
            public IStorageItemViewModel Data { get; set; }
        }
        
        public async Task<IGetStorageItemByIdResultContract> Ask(
            IGetStorageItemByIdRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = (await _context.StorageItems
                    .FirstOrDefaultAsync(c => c.Id == input.Id, cancellationToken: cancellationToken))
                .MapTo<IStorageItemViewModel>();

            return new GetStorageItemByIdSuccessResultContract() {Data = result};
        }
    }   
}