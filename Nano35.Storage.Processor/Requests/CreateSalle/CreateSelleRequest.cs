using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.CreateSalle
{
    public class CreateSelleRequest :
        IPipelineNode<ICreateSelleRequestContract, ICreateSelleResultContract>
    {
        private readonly ApplicationContext _context;

        public CreateSelleRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class CreateSelleSuccessResultContract : 
            ICreateSelleSuccessResultContract
        {
            
        }
        
        public async Task<ICreateSelleResultContract> Ask(
            ICreateSelleRequestContract input,
            CancellationToken cancellationToken)
        {
            // ToDo !!!
                    
            return new CreateSelleSuccessResultContract();
        }
    }
}