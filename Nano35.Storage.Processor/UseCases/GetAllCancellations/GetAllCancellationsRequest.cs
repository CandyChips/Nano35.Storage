using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
        
        private class GetAllCancellationsSuccessResultContract : 
            IGetAllCancellationsSuccessResultContract
        {
            public List<CancellationViewModel> Data { get; set; }
        }
        
        public async Task<IGetAllCancellationsResultContract> Ask
            (IGetAllCancellationsRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = await _context
                .Cancellations
                .MapAllToAsync<CancellationViewModel>();

            return new GetAllCancellationsSuccessResultContract() {Data = result};
        }
    }   
}