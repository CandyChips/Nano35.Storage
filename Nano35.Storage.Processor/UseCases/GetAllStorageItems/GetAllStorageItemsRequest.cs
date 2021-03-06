﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.files;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllStorageItems
{
    public class GetAllStorageItemsRequest : UseCaseEndPointNodeBase<IGetAllStorageItemsRequestContract, IGetAllStorageItemsResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetAllStorageItemsRequest(ApplicationContext context, IBus bus)
        {
            _context = context;
            _bus = bus;
        }
        
        public override async Task<UseCaseResponse<IGetAllStorageItemsResultContract>> Ask
            (IGetAllStorageItemsRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = await _context
                .StorageItems
                .Where(c => c.InstanceId == input.InstanceId)
                .Select(a => 
                    new StorageItemViewModel()
                    {Id = a.Id,
                     Article = a.Article.ToString(),
                     Comment = a.Comment,
                     Condition = a.Condition.Name,
                     HiddenComment = a.HiddenComment,
                     PurchasePrice = a.PurchasePrice,
                     RetailPrice = a.RetailPrice,
                     //Images = (new GetImagesOfStorageItem(_bus,new GetImagesOfStorageItemRequestContract() {StorageItemId = a.Id})
                     //    .GetResponse()
                     //    .Result as IGetImagesOfStorageItemSuccessResultContract)
                     //    .Images
                     })
                .ToListAsync(cancellationToken: cancellationToken);

            return new UseCaseResponse<IGetAllStorageItemsResultContract>(new GetAllStorageItemsResultContract()
                {Data = result});
        }
    }   
}