﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllArticles
{
    public class GetAllArticlesRequest :
        UseCaseEndPointNodeBase<IGetAllArticlesRequestContract, IGetAllArticlesResultContract>
    {
        private readonly ApplicationContext _context;
        public GetAllArticlesRequest(ApplicationContext context) { _context = context; }

        public override async Task<UseCaseResponse<IGetAllArticlesResultContract>> Ask(
            IGetAllArticlesRequestContract input, CancellationToken cancellationToken) =>
            new(new GetAllArticlesResultContract()
            {
                Data =
                    await _context
                        .Articles
                        .Where(c => c.InstanceId == input.InstanceId)
                        .Select(a =>
                            new ArticleViewModel()
                            {
                                Brand = a.Brand,
                                Model = a.Model,
                                Category = a.Category.Name,
                                CategoryId = a.CategoryId,
                                Id = a.Id,
                                Info = a.Info,
                                Specs = a
                                    .Specs
                                    .Select(e => new SpecViewModel() {Key = e.Key, Value = e.Value})
                                    .ToList()
                            })
                        .ToListAsync(cancellationToken: cancellationToken)
            });
    }   
}