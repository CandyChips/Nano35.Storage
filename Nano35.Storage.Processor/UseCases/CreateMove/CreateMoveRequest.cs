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

namespace Nano35.Storage.Processor.UseCases.CreateMove
{
    public class CreateMoveRequest :
        UseCaseEndPointNodeBase<ICreateMoveRequestContract, ICreateMoveResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;
        public CreateMoveRequest(ApplicationContext context, IBus bus) { _context = context; _bus = bus; }
        public override async Task<UseCaseResponse<ICreateMoveResultContract>> Ask(
            ICreateMoveRequestContract input,
            CancellationToken cancellationToken)
        {
            if (!input.Details.Any())
                return new UseCaseResponse<ICreateMoveResultContract>("Нет деталей перемещения");
            if (input.InstanceId == Guid.Empty)
                return new UseCaseResponse<ICreateMoveResultContract>("Обновите страницу и попробуйте еще раз");
            if (input.NewId == Guid.Empty)
                return new UseCaseResponse<ICreateMoveResultContract>("Обновите страницу и попробуйте еще раз");
            
            var unitString = new MasstransitUseCaseRequest<IGetUnitStringByIdRequestContract, IGetUnitStringByIdResultContract>(_bus, new GetUnitStringByIdRequestContract() {UnitId = input.FromUnitId}).GetResponse().Result;
            var countNumber = await _context.Moves.Where(c => c.Date.Year == DateTime.Today.Year).CountAsync(cancellationToken);
            var number = $@"{unitString.Success.Data.Substring(0, 1)}{countNumber}";
            
            var move = new Move()
                {Id = input.NewId, 
                 Number = number, 
                 Date = DateTime.Now,
                 InstanceId = input.InstanceId};

            var moveDetails = input
                .Details
                .Select(a => new MoveDetail()
                    {Id = a.NewId,
                     Count = a.Count,
                     FromPlace = a.FromPlaceOnStorage,
                     FromUnitId = input.FromUnitId,
                     MoveId = input.NewId,
                     StorageItemId = a.StorageItemId,
                     ToPlace = a.ToPlaceOnStorage,
                     ToUnitId = input.ToUnitId})
                .ToList();
            foreach (var item in input.Details)
            {
                if(_context.Warehouses
                    .Any(a =>
                        a.Name == item.ToPlaceOnStorage &&
                        a.StorageItemId == item.StorageItemId && 
                        a.UnitId == input.ToUnitId))
                {
                    var fromWh = (await _context.Warehouses
                        .FirstOrDefaultAsync(a =>
                            a.Name == item.FromPlaceOnStorage &&
                            a.StorageItemId == item.StorageItemId &&
                            a.UnitId == input.FromUnitId, cancellationToken: cancellationToken));
                    fromWh.Count -= item.Count;
                    
                    var toWh = (await _context.Warehouses
                        .FirstOrDefaultAsync(a =>
                            a.Name == item.ToPlaceOnStorage &&
                            a.StorageItemId == item.StorageItemId &&
                            a.UnitId == input.ToUnitId, cancellationToken: cancellationToken));
                    toWh.Count += item.Count;
                }
                else
                {
                    var warehouse = new WarehouseByItemOnStorage()
                        {Count = item.Count,
                         InstanceId = input.InstanceId,
                         IsDeleted = false,
                         Name = item.ToPlaceOnStorage,
                         StorageItemId = item.StorageItemId,
                         UnitId = input.ToUnitId};
                    await _context.Warehouses.AddAsync(warehouse, cancellationToken);
                }
            }
            
            await _context.Moves.AddAsync(move, cancellationToken);
            await _context.MoveDetails.AddRangeAsync(moveDetails, cancellationToken);
            return new UseCaseResponse<ICreateMoveResultContract>(new CreateMoveResultContract());
        }
    }
}