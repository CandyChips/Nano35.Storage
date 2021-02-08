using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.CreateCancelation
{
    public class CreateCancelationRequest :
        IPipelineNode<ICreateCancelationRequestContract, ICreateCancelationResultContract>
    {
        private readonly ApplicationContext _context;

        public CreateCancelationRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class CreateCancelationSuccessResultContract : 
            ICreateCancelationSuccessResultContract
        {
            
        }
        
        public async Task<ICreateCancelationResultContract> Ask(
            ICreateCancelationRequestContract input,
            CancellationToken cancellationToken)
        {
            // ToDo !!!
                    
            return new CreateCancelationSuccessResultContract();
        }
    }
}