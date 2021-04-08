using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllArticleCategories
{
    public class GetAllArticlesCategoriesRequest :
        EndPointNodeBase<IGetAllArticlesCategoriesRequestContract, IGetAllArticlesCategoriesResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllArticlesCategoriesRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        public override async Task<IGetAllArticlesCategoriesResultContract> Ask
            (IGetAllArticlesCategoriesRequestContract input, CancellationToken cancellationToken)
        {
            var result = input.ParentId == Guid.Empty
                ? await _context.Categories
                    .Where(c => c.ParentCategoryId == input.ParentId && c.ParentCategoryId == null)
                    .Select(a =>
                        new CategoryViewModel()
                        {
                            Id = a.Id,
                            Name = a.Name,
                            ParentCategoryId = a.ParentCategoryId.Value
                        })
                    .ToListAsync(cancellationToken: cancellationToken)
                : await _context.Categories
                    .Where(c => c.ParentCategoryId == input.ParentId)
                    .Select(a =>
                        new CategoryViewModel()
                        {
                            Id = a.Id,
                            Name = a.Name,
                            ParentCategoryId = a.ParentCategoryId.Value
                        })
                    .ToListAsync(cancellationToken: cancellationToken);

            return new GetAllArticlesCategoriesSuccessResultContract() {Data = result};
        }
    }   
}