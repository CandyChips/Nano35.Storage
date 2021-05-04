using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllArticleCategories
{
    public class GetAllArticlesCategoriesRequest :
        UseCaseEndPointNodeBase<IGetAllArticlesCategoriesRequestContract, IGetAllArticlesCategoriesResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllArticlesCategoriesRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        public override async Task<UseCaseResponse<IGetAllArticlesCategoriesResultContract>> Ask(
            IGetAllArticlesCategoriesRequestContract input, CancellationToken cancellationToken)
        {
            List<CategoryViewModel> result;

            if (input.ParentId == Guid.Empty)
            {
                result = await _context.Categories
                    .Where(c => c.InstanceId == input.InstanceId && c.ParentCategory == null)
                    .Select(a =>
                        new CategoryViewModel()
                        {
                            Id = a.Id,
                            Name = a.Name
                        })
                    .ToListAsync(cancellationToken: cancellationToken);
            }
            else
            {
                result = await _context.Categories
                    .Where(c => c.ParentCategoryId == input.ParentId)
                    .Select(a =>
                        new CategoryViewModel()
                        {
                            Id = a.Id,
                            Name = a.Name,
                            ParentCategoryId = a.ParentCategoryId.Value
                        })
                    .ToListAsync(cancellationToken: cancellationToken);
            }

            return new UseCaseResponse<IGetAllArticlesCategoriesResultContract>(
                new GetAllArticlesCategoriesResultContract() {Data = result});
        }
    }   
}