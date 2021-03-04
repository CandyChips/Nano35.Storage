using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CreateSalle
{
    public class CreateSelleRequest :
        IPipelineNode<
            ICreateSelleRequestContract, 
            ICreateSelleResultContract>
    {
        private readonly ApplicationContext _context;

        public CreateSelleRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class CreateSelleSuccessResultContract : 
            ICreateSelleSuccessResultContract
        {
            
        }
        
        public async Task<ICreateSelleResultContract> Ask(
            ICreateSelleRequestContract input,
            CancellationToken cancellationToken)
        {
            var cashOperationId = Guid.NewGuid();
            
            var selle = new Selle()
            {
                Id = input.NewId,
                CashOperationId = cashOperationId,
                ClientId = input.ClientId,
                Date = DateTime.Now,
                Number = input.Number
            };

            var selleDetails = input.Details
                .Select(a => new SelleDetail()
                {
                    Id = a.NewId,
                    Count = a.Count,
                    FromPlace = a.PlaceOnStorage,
                    FromUnitId = input.UnitId,
                    Price = a.Price,
                    SelleId = input.NewId,
                    StorageItemId = a.StorageItemId
                });
            
            foreach (var item in input.Details)
            {
                if(_context.Warehouses
                    .Any(a =>
                        a.Name == item.PlaceOnStorage &&
                        a.StorageItemId == item.StorageItemId && 
                        a.UnitId == input.UnitId))
                {
                    var wh = (await _context.Warehouses
                        .FirstOrDefaultAsync(a =>
                            a.Name == item.PlaceOnStorage &&
                            a.StorageItemId == item.StorageItemId &&
                            a.UnitId == input.UnitId, cancellationToken: cancellationToken));
                    wh.Count += item.Count;
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            await _context.Sells.AddAsync(selle, cancellationToken);
            
            await _context.SelleDetails.AddRangeAsync(selleDetails, cancellationToken);
            
            return new CreateSelleSuccessResultContract();
        }
    }
}