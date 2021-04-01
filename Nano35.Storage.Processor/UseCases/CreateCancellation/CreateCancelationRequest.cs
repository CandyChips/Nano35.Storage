using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CreateCancellation
{
    public class CreateCancellationRequest :
        IPipelineNode<
            ICreateCancellationRequestContract, 
            ICreateCancellationResultContract>
    {
        private readonly ApplicationContext _context;

        public CreateCancellationRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class CreateCancellationSuccessResultContract : 
            ICreateCancellationSuccessResultContract
        {
            
        }
        
        public async Task<ICreateCancellationResultContract> Ask(
            ICreateCancellationRequestContract input,
            CancellationToken cancellationToken)
        {

            var cancellation = new Cancellation()
            {
                Id = input.NewId,
                Date = DateTime.Now,
                Number = input.Number,
                InstanceId = input.InstanceId,
                Comment = input.Comment
            };

            var cancellationDetails = input.Details
                .Select(a => new CancelationDetail()
                {
                    CancellationsId = input.NewId,
                    Cancellation = cancellation,
                    StorageItemId = a.StorageItemId,
                    Count = a.Count, 
                    FromPlace = a.PlaceOnStorage,
                    FromUnitId = input.UnitId,
                    Id = a.NewId,
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
                    wh.Count -= item.Count;
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            await _context.Cancellations.AddAsync(cancellation, cancellationToken);
            
            await _context.CancellationsDetails.AddRangeAsync(cancellationDetails, cancellationToken);
                    
            return new CreateCancellationSuccessResultContract();
        }
    }
}