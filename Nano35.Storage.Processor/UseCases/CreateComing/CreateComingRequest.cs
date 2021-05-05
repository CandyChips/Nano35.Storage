using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Cashbox.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CreateComing
{
    public class CreateComingRequest :
        UseCaseEndPointNodeBase<ICreateComingRequestContract, ICreateComingResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public CreateComingRequest(
            ApplicationContext context, IBus bus)
        {
            _context = context;
            _bus = bus;
        }
        
        public override async Task<UseCaseResponse<ICreateComingResultContract>> Ask(
            ICreateComingRequestContract input,
            CancellationToken cancellationToken)
        {
            if (!input.Details.Any())
                return new UseCaseResponse<ICreateComingResultContract>("Нет деталей оприходования");
            if (input.ClientId == Guid.Empty)
                return new UseCaseResponse<ICreateComingResultContract>("Не выбран клиент");
            if (input.InstanceId == Guid.Empty)
                return new UseCaseResponse<ICreateComingResultContract>("Обновите страницу и попробуйте еще раз");
            if (input.NewId == Guid.Empty)
                return new UseCaseResponse<ICreateComingResultContract>("Обновите страницу и попробуйте еще раз");
            
            var coming = new Coming()
                {Id = input.NewId,
                 ClientId = input.ClientId,
                 Date = DateTime.Now,
                 Number = input.Number,
                 InstanceId = input.InstanceId};
            
            var comingDetails = input.Details
                .Select(a =>
                    new ComingDetail()
                        {ComingId = input.NewId,
                         StorageItemId = a.StorageItemId, 
                         ToPlace = a.PlaceOnStorage,
                         Count = a.Count, 
                         Price = a.Price,
                         ToUnitId = input.UnitId})
                .ToList();
            
            foreach (var item in input.Details)
            {
                if(_context.Warehouses.Any(a => a.Name == item.PlaceOnStorage && a.StorageItemId == item.StorageItemId && a.UnitId == input.UnitId))
                {
                    var wh = await _context.Warehouses
                        .FirstOrDefaultAsync(a => a.Name == item.PlaceOnStorage && a.StorageItemId == item.StorageItemId && a.UnitId == input.UnitId, cancellationToken: cancellationToken);
                    wh.Count += item.Count;
                }
                else
                {
                    var warehouse = new WarehouseByItemOnStorage()
                        {Count = item.Count,
                         InstanceId = input.InstanceId,
                         IsDeleted = false,
                         Name = item.PlaceOnStorage,
                         StorageItemId = item.StorageItemId,
                         UnitId = input.UnitId};
                    await _context.Warehouses.AddAsync(warehouse, cancellationToken);
                }
            }

            

            await new MasstransitUseCaseRequest<ICreateComingCashOperationRequestContract,
                ICreateComingCashOperationResultContract>(_bus,
                new CreateComingCashOperationRequestContract()
                {
                    NewId = Guid.NewGuid(),
                    CashboxId = input.UnitId,
                    ComingId = input.NewId,
                    Cash = input.Details.Select(a => a.Price * a.Count).Sum(),
                    Description = "Оплата оприходования."
                }).GetResponse();
            
            await _context.Comings.AddAsync(coming, cancellationToken);
            
            await _context.ComingDetails.AddRangeAsync(comingDetails, cancellationToken);

            return new UseCaseResponse<ICreateComingResultContract>(new CreateComingResultContract());
        }
    }
}