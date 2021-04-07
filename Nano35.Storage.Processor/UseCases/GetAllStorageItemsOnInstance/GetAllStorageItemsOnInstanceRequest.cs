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
        EndPointNodeBase<
            IGetAllStorageItemsOnInstanceContract,
            IGetAllStorageItemsOnInstanceResultContract>
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
            var queue = await _context
                .Warehouses
                .Where(c =>
                    c.InstanceId == input.InstanceId).ToListAsync(cancellationToken);

            var result = queue
                .GroupBy(g => g.StorageItem, e => e)
                .Select(a =>
                {
                    var res = new StorageItemOnInstanceViewModel()
                    {
                        Count = a.Sum(s => s.Count),
                    };
                    var getAllStorageItems = new Requests.GetAllStorageItems(_bus,
                        new GetAllStorageItemsRequestContract() {InstanceId = a.Key.InstanceId});
                    res.Item = getAllStorageItems.GetResponse().Result switch
                    {
                        IGetAllStorageItemsSuccessResultContract success =>
                            success.Data.MapTo<StorageItemWarehouseView>(),
                        _ => throw new Exception()
                    };
                    var getUnitStringById = new GetUnitStringById(_bus,
                        new GetUnitStringByIdRequestContract() {UnitId = a.FirstOrDefault().UnitId});
                    res.Unit = getUnitStringById.GetResponse().Result switch
                    {
                        IGetUnitStringByIdSuccessResultContract success => success.Data,
                        _ => throw new Exception()
                    };
                    res.Item = new StorageItemWarehouseView()
                    {
                        Id = a.Key.Id,
                        Name = a.Key.ToString(),
                        PurchasePrice = (double) (a.Key.PurchasePrice),
                        RetailPrice = (double) (a.Key.RetailPrice),
                    };
                    return res;
                }).ToList();

            return new GetAllStorageItemsOnInstanceSuccessResultContract() {Contains = result};
        }
    }
}