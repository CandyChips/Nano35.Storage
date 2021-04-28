﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllArticleModels
{
    public class GetAllArticlesModelsRequest :
        EndPointNodeBase<IGetAllArticlesModelsRequestContract, IGetAllArticlesModelsResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllArticlesModelsRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        public override async Task<IGetAllArticlesModelsResultContract> Ask(IGetAllArticlesModelsRequestContract input, CancellationToken cancellationToken) =>
            new GetAllArticlesModelsSuccessResultContract()
            {
                Data = 
                    await _context
                        .Articles
                        .Where(c => c.CategoryId == input.CategoryId && c.CategoryId == input.CategoryId)
                        .Select(a => a.Model)
                        .Distinct()
                        .ToListAsync(cancellationToken: cancellationToken)
            };
    }   
}