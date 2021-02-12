using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.GetAllArticleCategories
{
    public class GetAllArticlesCategoriesRequest :
        IPipelineNode<
            IGetAllArticlesCategoriesRequestContract,
            IGetAllArticlesCategoriesResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllArticlesCategoriesRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetAllArticlesCategoriesSuccessResultContract : 
            IGetAllArticlesCategoriesSuccessResultContract
        {
            public IEnumerable<ICategoryViewModel> Data { get; set; }
        }
        
        public async Task<IGetAllArticlesCategoriesResultContract> Ask
            (IGetAllArticlesCategoriesRequestContract input, CancellationToken cancellationToken)
        {
            var result = input.ParentId == Guid.Empty
                ? await _context.Categories.Where(c => c.InstanceId == input.InstanceId && c.ParentCategoryId == null)
                    .MapAllToAsync<ICategoryViewModel>()
                : await _context.Categories.Where(c => c.ParentCategoryId == input.ParentId)
                    .MapAllToAsync<ICategoryViewModel>();

            return new GetAllArticlesCategoriesSuccessResultContract() {Data = result};
        }
    }   
}