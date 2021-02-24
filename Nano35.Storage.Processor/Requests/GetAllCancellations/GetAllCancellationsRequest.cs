using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.GetAllCancellations
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
        
        private class GetAllCancellationsSuccessResultContract : 
            IGetAllCancellationsSuccessResultContract
        {
            public IEnumerable<IComingViewModel> Data { get; set; }
        }
        
        public async Task<IGetAllCancellationsResultContract> Ask
            (IGetAllCancellationsRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = await _context
                .Cancellations
                .MapAllToAsync<IComingViewModel>();

            return new GetAllCancellationsSuccessResultContract() {Data = result};
        }
    }   
}