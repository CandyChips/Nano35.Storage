﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemArticle
{
    public class UpdateStorageItemArticleRequest :
        UseCaseEndPointNodeBase<IUpdateStorageItemArticleRequestContract, IUpdateStorageItemArticleResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateStorageItemArticleRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        public override async Task<UseCaseResponse<IUpdateStorageItemArticleResultContract>> Ask(
            IUpdateStorageItemArticleRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.StorageItems.FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken);
            
            if (result == null)
                return Pass("Не найдено");
            
            result.ArticleId = input.ArticleId;

            return Pass(new UpdateStorageItemArticleResultContract());
        }
    }
}