using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Initializers;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.files;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.PresentationGetAllStorageItems
{
    public class PresentationGetAllStorageItemsRequest :
        UseCaseEndPointNodeBase<IPresentationGetAllStorageItemsRequestContract, IPresentationGetAllStorageItemsResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public PresentationGetAllStorageItemsRequest(
            ApplicationContext context, IBus bus)
        {
            _context = context;
            _bus = bus;
        }
        
        public override async Task<UseCaseResponse<IPresentationGetAllStorageItemsResultContract>> Ask(
            IPresentationGetAllStorageItemsRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = await _context
                .StorageItems
                .Where(c => c.InstanceId == input.InstanceId)
                .Select(a => new PresentationStorageItemViewModel()
                {
                    Id = a.Id,
                    ArticleId = a.ArticleId,
                    Comment = a.Comment,
                    PurchasePrice = a.PurchasePrice
                })
                .ToListAsync(cancellationToken);
            result.ForEach(e =>
            {
                var getClientStringRequest = new GetImagesOfStorageItem(_bus, new GetImagesOfStorageItemRequestContract() { StorageItemId = e.Id });
                e.Images = getClientStringRequest.GetResponse().Result switch
                {
                    IGetImagesOfStorageItemSuccessResultContract success => success.Images,
                    _ => throw new Exception()
                };
            });
            return Pass(new PresentationGetAllStorageItemsResultContract() {StorageItems = result});
        }
    }   
}