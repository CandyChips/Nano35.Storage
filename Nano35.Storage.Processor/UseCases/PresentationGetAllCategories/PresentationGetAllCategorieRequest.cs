using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit.Initializers;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.PresentationGetAllCategories
{
    public class PresentationGetAllCategoriesRequest :
        EndPointNodeBase<IPresentationGetAllCategoriesRequestContract, IPresentationGetAllCategoriesResultContract>
    {
        private readonly ApplicationContext _context;

        public PresentationGetAllCategoriesRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        public override async Task<IPresentationGetAllCategoriesResultContract> Ask(
            IPresentationGetAllCategoriesRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = await _context
                .Categories
                .Where(c => c.InstanceId == input.InstanceId)
                .Select(a => new PresentationCategoryViewModel()
                {
                    Id = a.Id,
                    Name = a.Name, 
                    ParentCategoryId = a.ParentCategoryId ?? Guid.Empty
                })
                .ToListAsync(cancellationToken: cancellationToken);

            return new PresentationGetAllCategoriesSuccessResultContract() {Categories = result};
        }
    }   
}