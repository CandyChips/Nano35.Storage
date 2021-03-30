﻿using System;
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

namespace Nano35.Storage.Processor.UseCases.GetAllStorageItemsOnUnit
{
    public class GetAllStorageItemsOnUnitRequest :
        IPipelineNode<
            IGetAllStorageItemsOnUnitContract,
            IGetAllStorageItemsOnUnitResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetAllStorageItemsOnUnitRequest(
            ApplicationContext context,
            IBus bus)
        {
            _context = context;
            _bus = bus;
        }
        
        private class GetAllStorageItemsOnUnitSuccessResultContract : 
            IGetAllStorageItemsOnUnitSuccessResultContract
        {
            public List<StorageItemOnUnitViewModel> Contains { get; set; }
        }
        
        public async Task<IGetAllStorageItemsOnUnitResultContract> Ask
            (IGetAllStorageItemsOnUnitContract input, 
            CancellationToken cancellationToken)
        {
            var queue = await _context
                .Warehouses
                .Where(c =>
                    c.UnitId == input.UnitId).ToListAsync(cancellationToken);


            var result = queue
                .Select(a =>
                {
                    var res = new StorageItemOnUnitViewModel()
                    {
                        Count = a.Count,
                        Place = a.Name
                    };
                    var getAllStorageItems = new GetAllStorageItems(_bus,
                        new GetAllStorageItemsRequestContract() {InstanceId = a.InstanceId});
                    res.Id = getAllStorageItems.GetResponse().Result switch
                    {
                        IGetAllStorageItemsSuccessResultContract success => success.Data.MapTo<StorageItemViewModel>(),
                        _ => throw new Exception()
                    };

                    return res;
                }).ToList();

            return new GetAllStorageItemsOnUnitSuccessResultContract() {Contains = result};
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