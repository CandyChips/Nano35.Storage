using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.CreateCancellation
{
    public class CreateCancellationRequest :
        IPipelineNode<
            ICreateCancellationRequestContract, 
            ICreateCancellationResultContract>
    {
        private readonly ApplicationContext _context;

        public CreateCancellationRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class CreateCancellationSuccessResultContract : 
            ICreateCancellationSuccessResultContract
        {
            
        }
        
        public async Task<ICreateCancellationResultContract> Ask(
            ICreateCancellationRequestContract input,
            CancellationToken cancellationToken)
        {
            // ToDo !!!
                    
            return new CreateCancellationSuccessResultContract();
        }
    }
}