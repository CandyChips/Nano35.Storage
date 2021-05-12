using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CreateCancellation
{
    public class CreateCancellationRequest : UseCaseEndPointNodeBase<ICreateCancellationRequestContract, ICreateCancellationResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;
        public CreateCancellationRequest(ApplicationContext context, IBus bus) { _context = context; _bus = bus; }
        public override async Task<UseCaseResponse<ICreateCancellationResultContract>> Ask(
            ICreateCancellationRequestContract input,
            CancellationToken cancellationToken)
        {
            var unitString = new MasstransitUseCaseRequest<IGetUnitStringByIdRequestContract, IGetUnitStringByIdResultContract>(_bus, new GetUnitStringByIdRequestContract() {UnitId = input.UnitId}).GetResponse().Result;
            var countNumber = await _context.Cancellations.Where(c => c.Date.Year == DateTime.Today.Year).CountAsync(cancellationToken);
            var number = $@"{unitString.Success.Data.Substring(0, 1)}{countNumber}";
            
            var cancellation = 
                new Cancellation()
                    {Id = input.NewId,
                     Date = DateTime.Now,
                     Number = number,
                     InstanceId = input.InstanceId,
                     Comment = input.Comment ?? ""};

            var cancellationDetails = input
                .Details
                .Select(a => 
                    new CancelationDetail()
                        {CancellationsId = input.NewId,
                         Cancellation = cancellation,
                         StorageItemId = a.StorageItemId,
                         Count = a.Count, 
                         FromPlace = a.PlaceOnStorage,
                         FromUnitId = input.UnitId,
                         Id = a.NewId});

            if (input.NewId == Guid.Empty) return Pass("");
            if (input.InstanceId == Guid.Empty) return Pass("");
            
            foreach (var item in input.Details)
            {
                if(_context
                    .Warehouses
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
                    return Pass("");
                }
            }

            await _context.Cancellations.AddAsync(cancellation, cancellationToken);
            
            await _context.CancellationsDetails.AddRangeAsync(cancellationDetails, cancellationToken);

            return Pass(new CreateCancellationResultContract());
        }
    }
}