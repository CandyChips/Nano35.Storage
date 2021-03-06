﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetArticleById
{
    public class GetArticleByIdRequest :
        UseCaseEndPointNodeBase<IGetArticleByIdRequestContract, IGetArticleByIdResultContract>
    {
        private readonly ApplicationContext _context;

        public GetArticleByIdRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        public override async Task<UseCaseResponse<IGetArticleByIdResultContract>> Ask(
            IGetArticleByIdRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = await _context.Articles
                .FirstAsync(c => c.Id == input.Id, cancellationToken: cancellationToken);

            if (result == null)
                return Pass("Не найдено");
            
            var article = new ArticleViewModel()
            {
                Brand = result.Brand,
                Model = result.Model,
                Category = result.Category.ToString(),
                CategoryId = result.CategoryId,
                Id = result.Id,
                Info = result.Info
            };

            return new UseCaseResponse<IGetArticleByIdResultContract>(
                new GetArticleByIdResultContract() {Data = article});
        }
    }   
}