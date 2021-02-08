using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.CreateStorageItem
{
    public class CreateStorageItemRequest :
        IPipelineNode<ICreateStorageItemRequestContract, ICreateStorageItemResultContract>
    {
        private readonly ApplicationContext _context;

        public CreateStorageItemRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class CreateStorageItemSuccessResultContract : 
            ICreateStorageItemSuccessResultContract
        {
            
        }
        
        public async Task<ICreateStorageItemResultContract> Ask(
            ICreateStorageItemRequestContract input,
            CancellationToken cancellationToken)
        {
            // ToDo !!!
                    
            return new CreateStorageItemSuccessResultContract();
        }
    }
}