﻿using System;
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

namespace Nano35.Storage.Processor.UseCases.CreateSalle
{
    public class CreateSelleRequest :
        UseCaseEndPointNodeBase<ICreateSelleRequestContract, ICreateSelleResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public CreateSelleRequest(ApplicationContext context, IBus bus)
        {
            _context = context;
            _bus = bus;
        }
        
        public override async Task<UseCaseResponse<ICreateSelleResultContract>> Ask(
            ICreateSelleRequestContract input,
            CancellationToken cancellationToken)
        {
            if (!input.Details.Any())
                return new UseCaseResponse<ICreateSelleResultContract>("Нет деталей продажи");
            if (input.InstanceId == Guid.Empty)
                return new UseCaseResponse<ICreateSelleResultContract>("Обновите страницу и попробуйте еще раз");
            if (input.NewId == Guid.Empty)
                return new UseCaseResponse<ICreateSelleResultContract>("Обновите страницу и попробуйте еще раз");
            
            var cashOperationId = Guid.NewGuid();
            
            var selle = new Selle()
                {Id = input.NewId,
                 CashOperationId = cashOperationId,
                 Date = DateTime.Now,
                 Number = input.Number,
                 InstanceId = input.InstanceId};

            var selleDetails = input
                .Details
                .Select(a => 
                    new SelleDetail()
                        {Id = a.NewId,
                         Count = a.Count,
                         FromPlace = a.PlaceOnStorage,
                         FromUnitId = input.UnitId,
                         Price = a.Price,
                         SelleId = input.NewId,
                         StorageItemId = a.StorageItemId});
            
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
            

            await new MasstransitUseCaseRequest<ICreateSelleCashOperationRequestContract,
                ICreateSelleCashOperationResultContract>(_bus,
                new CreateSelleCashOperationRequestContract()
                {
                    NewId = Guid.NewGuid(),
                    CashboxId = input.UnitId,
                    SelleId = input.NewId,
                    Cash = input.Details.Select(a => a.Price * a.Count).Sum(),
                    Description = "Продажа"
                }).GetResponse();

            await _context.Sells.AddAsync(selle, cancellationToken);
            await _context.SelleDetails.AddRangeAsync(selleDetails, cancellationToken);
            return new UseCaseResponse<ICreateSelleResultContract>(new CreateSelleResultContract());
        }
    }
}