using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.CreateComing
{
    public class CreateMoveRequest :
        IPipelineNode<ICreateMoveRequestContract, ICreateMoveResultContract>
    {
        private readonly ApplicationContext _context;

        public CreateMoveRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class CreateMoveSuccessResultContract : 
            ICreateMoveSuccessResultContract
        {
            
        }
        
        public async Task<ICreateMoveResultContract> Ask(
            ICreateMoveRequestContract input,
            CancellationToken cancellationToken)
        {
            // ToDo !!!
                    
            return new CreateMoveSuccessResultContract();
        }
    }
}