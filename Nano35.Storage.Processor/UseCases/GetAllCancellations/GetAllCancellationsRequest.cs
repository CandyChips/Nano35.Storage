using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllCancellations
{
    public class GetAllCancellationsRequest :
        IPipelineNode<
            IGetAllCancellationsRequestContract,
            IGetAllCancellationsResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllCancellationsRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        public async Task<IGetAllCancellationsResultContract> Ask
            (IGetAllCancellationsRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = await _context
                .Cancellations
                .Where(w => w.InstanceId == input.InstanceId)
                .Select(a => new CancellationViewModel() { })
                .ToListAsync(cancellationToken: cancellationToken);

            return new GetAllCancellationsSuccessResultContract() {Data = result};
        }
    }   
}