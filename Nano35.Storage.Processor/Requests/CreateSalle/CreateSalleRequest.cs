using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.CreateSalle
{
    public class CreateSalleRequest :
        IPipelineNode<ICreateSalleRequestContract, ICreateSalleResultContract>
    {
        private readonly ApplicationContext _context;

        public CreateSalleRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class CreateSalleSuccessResultContract : 
            ICreateSalleSuccessResultContract
        {
            
        }
        
        public async Task<ICreateSalleResultContract> Ask(
            ICreateSalleRequestContract input,
            CancellationToken cancellationToken)
        {
            // ToDo !!!
                    
            return new CreateSalleSuccessResultContract();
        }
    }
}