using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetArticleById
{
    public class GetArticleByIdRequest :
        EndPointNodeBase<
            IGetArticleByIdRequestContract,
            IGetArticleByIdResultContract>
    {
        private readonly ApplicationContext _context;

        public GetArticleByIdRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetArticleByIdSuccessResultContract : 
            IGetArticleByIdSuccessResultContract
        {
            public ArticleViewModel Data { get; set; }
        }
        
        public override async Task<IGetArticleByIdResultContract> Ask(
            IGetArticleByIdRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = _context.Articles
                .FirstOrDefault(c => c.Id == input.Id)
                .MapTo<ArticleViewModel>();

            return new GetArticleByIdSuccessResultContract() {Data = result};
        }
    }   
}