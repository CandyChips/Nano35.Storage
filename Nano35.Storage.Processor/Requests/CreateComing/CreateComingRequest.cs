using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.CreateComing
{
    public class CreateComingRequest :
        IPipelineNode<ICreateComingRequestContract, ICreateComingResultContract>
    {
        private readonly ApplicationContext _context;

        public CreateComingRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class CreateComingSuccessResultContract : 
            ICreateComingSuccessResultContract
        {
            
        }
        
        public async Task<ICreateComingResultContract> Ask(
            ICreateComingRequestContract input,
            CancellationToken cancellationToken)
        {
            var coming = new Coming()
            {
                Id = input.NewId,
                Date = DateTime.Now,
                Number = input.Number,
                InstanceId = input.InstanceId,
            };
            
            var comingDetails = input.Details
                .Select(a => new ComingDetail()
                {
                    ComingId = input.NewId,
                    StorageItemId = a.StorageItemId, 
                    ToPlace = a.PlaceOnStorage,
                    Count = a.Count, 
                    Price = a.Price,
                    ToUnitId = input.UnitId
                });
            
            foreach (var item in input.Details)
            {
                if(_context.Warehouses
                    .Any(a =>
                        a.StorageItemId == item.StorageItemId && 
                        a.UnitId == input.UnitId))
                {
                    var wh = (await _context.Warehouses
                        .FirstOrDefaultAsync(a =>
                            a.StorageItemId == item.StorageItemId &&
                            a.UnitId == input.UnitId, cancellationToken: cancellationToken));
                    wh.Count += item.Count;
                }
                else
                {
                    await _context.Warehouses.AddAsync(new WarehouseByItemOnStorage()
                    {
                        Count = item.Count,
                        InstanceId = input.InstanceId,
                        IsDeleted = false,
                        Name = item.PlaceOnStorage,
                        StorageItemId = item.StorageItemId,
                        UnitId = input.UnitId
                    }, cancellationToken);
                }
            }

            await _context.Comings.AddAsync(coming, cancellationToken);
            
            await _context.ComingDetails.AddRangeAsync(comingDetails, cancellationToken);
                    
            return new CreateComingSuccessResultContract();
        }
    }
}