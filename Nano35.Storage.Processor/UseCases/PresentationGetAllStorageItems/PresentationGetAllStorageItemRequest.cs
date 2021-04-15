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

namespace Nano35.Storage.Processor.UseCases.PresentationGetAllStorageItems
{
    public class PresentationGetAllStorageItemsRequest :
        EndPointNodeBase<IPresentationGetAllStorageItemsRequestContract, IPresentationGetAllStorageItemsResultContract>
    {
        private readonly ApplicationContext _context;

        public PresentationGetAllStorageItemsRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        public override async Task<IPresentationGetAllStorageItemsResultContract> Ask
            (IPresentationGetAllStorageItemsRequestContract input, CancellationToken cancellationToken)
        {
            var result = await _context
                .StorageItems
                .Where(c => c.InstanceId == input.InstanceId)
                .Select(a => new PresentationStorageItemViewModel()
                {
                    Id = a.Id,
                    ArticleId = a.ArticleId,
                    Comment = a.Comment,
                    ConditionId = a.ConditionId,
                    HiddenComment = a.HiddenComment,
                    PurchasePrice = a.PurchasePrice
                })
                .ToListAsync(cancellationToken: cancellationToken);

            return new PresentationGetAllStorageItemsSuccessResultContract() {StorageItems = result};
        }
    }   
}