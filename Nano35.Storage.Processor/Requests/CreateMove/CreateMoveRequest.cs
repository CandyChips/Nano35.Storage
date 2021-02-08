using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.CreateMove
{
    public class CreateComingRequest :
        IPipelineNode<ICreateComingRequestContract, ICreateComingResultContract>
    {
        private readonly ApplicationContext _context;

        public CreateComingRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class CreateComingSuccessResultContract : 
            ICreateComingSuccessResultContract
        {
            
        }
        
        public async Task<ICreateComingResultContract> Ask(
            ICreateComingRequestContract input,
            CancellationToken cancellationToken)
        {
            // ToDo !!!
                    
            return new CreateComingSuccessResultContract();
        }
    }
}