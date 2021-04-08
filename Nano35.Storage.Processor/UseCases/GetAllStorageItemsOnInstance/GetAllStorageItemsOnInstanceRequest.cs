using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Requests;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllStorageItemsOnInstance
{
    public class GetAllStorageItemsOnInstanceRequest :
        EndPointNodeBase<IGetAllStorageItemsOnInstanceContract, IGetAllStorageItemsOnInstanceResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetAllStorageItemsOnInstanceRequest(
            ApplicationContext context,
            IBus bus)
        {
            _context = context;
            _bus = bus;
        }
        
        public override async Task<IGetAllStorageItemsOnInstanceResultContract> Ask
            (IGetAllStorageItemsOnInstanceContract input, 
            CancellationToken cancellationToken)
        {
            var storageItem = (await _context
                .Warehouses
                .Where(c => c.InstanceId == input.InstanceId)
                .ToListAsync(cancellationToken))
                .GroupBy(g => g.StorageItem, e => e)
                .Select(a =>
                    new StorageItemOnInstanceViewModel
                    {
                        Count = a.Sum(s => s.Count),
                        Item = new StorageItemWarehouseView()
                        {
                            Id = a.Key.Id,
                            Name = a.Key.ToString(),
                            PurchasePrice = (double) (a.Key.PurchasePrice),
                            RetailPrice = (double) (a.Key.RetailPrice),
                        },
                        Unit = new GetUnitStringById(_bus, new GetUnitStringByIdRequestContract() {UnitId = a.First().UnitId}).GetResponse()
                                .Result switch
                            {
                                IGetUnitStringByIdSuccessResultContract success => success.Data,
                                _ => throw new Exception()
                            }
                    })
                .ToList();

            return new GetAllStorageItemsOnInstanceSuccessResultContract() {Contains = storageItem};
        }
    }   
}