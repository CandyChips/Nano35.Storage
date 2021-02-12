using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.GetAllComings
{
    public class GetAllComingsRequest :
        IPipelineNode<
            IGetAllComingsRequestContract,
            IGetAllComingsResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllComingsRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetAllComingsSuccessResultContract : 
            IGetAllComingsSuccessResultContract
        {
            public IEnumerable<IComingViewModel> Data { get; set; }
        }
        
        public async Task<IGetAllComingsResultContract> Ask
            (IGetAllComingsRequestContract input, CancellationToken cancellationToken)
        {
            var result = await _context
                .Comings
                .Where(c => c.InstanceId == input.InstanceId)
                .MapAllToAsync<IComingViewModel>();
            
            result.ForEach(e =>
            {
                e.Details = _context.ComingDetails
                    .Where(w => 
                        w.ComingId == e.Id)
                    .MapAllTo<IComingDetailViewModel>();
            });

            return new GetAllComingsSuccessResultContract() {Data = result};
        }
    }   
}