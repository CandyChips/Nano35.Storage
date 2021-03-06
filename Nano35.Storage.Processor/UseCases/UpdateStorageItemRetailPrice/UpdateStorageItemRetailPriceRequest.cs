﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemRetailPrice
{
    public class UpdateStorageItemRetailPriceRequest :
        UseCaseEndPointNodeBase<IUpdateStorageItemRetailPriceRequestContract, IUpdateStorageItemRetailPriceResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateStorageItemRetailPriceRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        public override async Task<UseCaseResponse<IUpdateStorageItemRetailPriceResultContract>> Ask(
            IUpdateStorageItemRetailPriceRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.StorageItems.FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken);
            
            if (result == null)
                return Pass("Не найдено");
            
            result.RetailPrice = input.RetailPrice;

            return Pass(new UpdateStorageItemRetailPriceResultContract());
        }
    }
}