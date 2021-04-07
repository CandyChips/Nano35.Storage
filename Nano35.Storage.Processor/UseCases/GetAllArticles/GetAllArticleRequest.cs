using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllArticles
{
    public class GetAllArticlesRequest :
        EndPointNodeBase<
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
            public List<ArticleViewModel> Data { get; set; }
        }

        public override async Task<IGetAllArticlesResultContract> Ask
            (IGetAllArticlesRequestContract input, CancellationToken cancellationToken)
        {
            var result = await _context
                .Articles
                .Where(c => c.InstanceId == input.InstanceId)
                .MapAllToAsync<ArticleViewModel>();

            return new GetAllArticlesSuccessResultContract() {Data = result};
        }
    }   
}