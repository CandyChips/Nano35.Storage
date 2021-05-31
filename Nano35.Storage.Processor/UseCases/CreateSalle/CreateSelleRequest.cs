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

namespace Nano35.Storage.Processor.UseCases.CreateSalle
{
    public class CreateSelleRequest :
        UseCaseEndPointNodeBase<ICreateSelleRequestContract, ICreateSelleResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;
        public CreateSelleRequest(ApplicationContext context, IBus bus) { _context = context; _bus = bus; }
        public override async Task<UseCaseResponse<ICreateSelleResultContract>> Ask(
            ICreateSelleRequestContract input,
            CancellationToken cancellationToken)
        {
            var unitString = new MasstransitUseCaseRequest<IGetUnitStringByIdRequestContract, IGetUnitStringByIdResultContract>(_bus, new GetUnitStringByIdRequestContract() {UnitId = input.UnitId}).GetResponse().Result;
            var countNumber = await _context.Sells.Where(c => c.Date.Year == DateTime.Today.Year).CountAsync(cancellationToken);
            var number = $@"{unitString.Success.Data.Substring(0, 1)}{countNumber}";
            
            var selle = new Selle()
                {Id = input.NewId,
                 Date = DateTime.Now,
                 Number = number,
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
                    return Pass("");
                }
            }
            

            await new MasstransitUseCaseRequest<ICreateSelleCashOperationRequestContract, ICreateSelleCashOperationResultContract>(_bus, new CreateSelleCashOperationRequestContract()
                    {NewId = Guid.NewGuid(),
                     CashboxId = input.UnitId,
                     SelleId = input.NewId,
                     Description = $"Оплата продажи №{selle.Number}"}).GetResponse();

            await _context.Sells.AddAsync(selle, cancellationToken);
            
            await _context.SelleDetails.AddRangeAsync(selleDetails, cancellationToken);
            
            return Pass(new CreateSelleResultContract());
        }
    }
}