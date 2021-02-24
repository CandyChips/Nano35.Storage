using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.GetAllArticles
{
    public class GetAllArticlesRequest :
        IPipelineNode<
            IGetAllArticlesRequestContract,
            IGetAllArticlesResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllArticlesRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetAllArticlesSuccessResultContract : 
            IGetAllArticlesSuccessResultContract
        {
            public IEnumerable<IArticleViewModel> Data { get; set; }
        }

        public async Task<IGetAllArticlesResultContract> Ask
            (IGetAllArticlesRequestContract input, CancellationToken cancellationToken)
        {
            var result = await _context
                .Articles
                .Where(c => c.InstanceId == input.InstanceId)
                .MapAllToAsync<IArticleViewModel>();

            return new GetAllArticlesSuccessResultContract() {Data = result};
        }
    }   
}