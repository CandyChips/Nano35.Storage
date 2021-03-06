﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateArticleBrand
{
    public class UpdateArticleBrandRequest : UseCaseEndPointNodeBase<IUpdateArticleBrandRequestContract, IUpdateArticleBrandResultContract>
    {
        private readonly ApplicationContext _context;
        public UpdateArticleBrandRequest(ApplicationContext context)
        {
            _context = context;
        }
        public override async Task<UseCaseResponse<IUpdateArticleBrandResultContract>> Ask(
            IUpdateArticleBrandRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await (_context.Articles.FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken));

            if (result == null)
                return Pass("Не найдено");
            
            result.Brand = input.Brand;
            return Pass(new UpdateArticleBrandResultContract());
        }
    }
}