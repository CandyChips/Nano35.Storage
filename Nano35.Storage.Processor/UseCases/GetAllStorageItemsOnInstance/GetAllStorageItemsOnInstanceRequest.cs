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
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllStorageItemsOnInstance
{
    public class GetAllStorageItemsOnInstanceRequest :
        IPipelineNode<
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
        
        private class GetAllStorageItemsOnInstanceSuccessResultContract : 
            IGetAllStorageItemsOnInstanceSuccessResultContract
        {
            public List<StorageItemOnInstanceViewModel> Contains { get; set; }
        }
        
        public async Task<IGetAllStorageItemsOnInstanceResultContract> Ask
            (IGetAllStorageItemsOnInstanceContract input, 
            CancellationToken cancellationToken)
        {
            var queue = await _context
                .Warehouses
                .Where(c =>
                    c.InstanceId == input.InstanceId).ToListAsync(cancellationToken);


            var result = queue
                .Select(a =>
                {
                    var res = new StorageItemOnInstanceViewModel()
                    {
                        Count = a.Count
                    };
                    var getAllStorageItems = new GetAllStorageItems(_bus,
                        new GetAllStorageItemsRequestContract() {InstanceId = a.InstanceId});
                    res.Id = getAllStorageItems.GetResponse().Result switch
                    {
                        IGetAllStorageItemsSuccessResultContract success => success.Data.MapTo<StorageItemViewModel>(),
                        _ => throw new Exception()
                    };
                    var getUnitStringById = new GetUnitStringById(_bus,
                        new GetUnitStringByIdRequestContract() {UnitId = a.UnitId});
                    res.Unit = getUnitStringById.GetResponse().Result switch
                    {
                        IGetUnitStringByIdSuccessResultContract success => success.Data,
                        IGetUnitStringByIdErrorResultContract => "",
                        _ => ""
                    };

                    return res;
                }).ToList();

            return new GetAllStorageItemsOnInstanceSuccessResultContract() {Contains = result};
        }
    }   
    
    public class GetAllStorageItems : 
        MasstransitRequest
        <IGetAllStorageItemsRequestContract, 
            IGetAllStorageItemsResultContract,
            IGetAllStorageItemsSuccessResultContract, 
            IGetAllStorageItemsErrorResultContract>
    {
        public GetAllStorageItems(IBus bus, IGetAllStorageItemsRequestContract request) : base(bus, request) {}
    }
}