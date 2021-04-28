using System;
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
using Nano35.Storage.Processor.Requests;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllStorageItemsOnUnit
{
    public class GetAllStorageItemsOnUnitRequest :
        EndPointNodeBase<IGetAllStorageItemsOnUnitContract, IGetAllStorageItemsOnUnitResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetAllStorageItemsOnUnitRequest(
            ApplicationContext context, IBus bus)
        {
            _context = context;
            _bus = bus;
        }
        
        public override async Task<IGetAllStorageItemsOnUnitResultContract> Ask(
            IGetAllStorageItemsOnUnitContract input, 
            CancellationToken cancellationToken)
        {
            var storageItems = (await _context
                .Warehouses
                .Where(c => c.UnitId == input.UnitId)
                .ToListAsync(cancellationToken))
                .Select(a =>
                    new StorageItemOnUnitViewModel
                    {Count = a.Count,
                     Item = 
                         new StorageItemWarehouseView()
                         {Id = a.StorageItem.Id,
                          Name = a.StorageItem.ToString(),
                          PurchasePrice = (double) a.StorageItem.PurchasePrice,
                          RetailPrice = (double) a.StorageItem.RetailPrice,
                          Images = (new GetImagesOfStorageItem(_bus,new GetImagesOfStorageItemRequestContract() { StorageItemId = a.StorageItem.Id })
                              .GetResponse()
                              .Result as IGetImagesOfStorageItemSuccessResultContract)?
                              .Images}})
                .ToList();

            return new GetAllStorageItemsOnUnitSuccessResultContract() {Contains = storageItems};
        }
    }   
}