using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllSells
{
    public class GetAllSellsRequest :
        UseCaseEndPointNodeBase<IGetAllSellsRequestContract, IGetAllSellsResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetAllSellsRequest(
            ApplicationContext context, 
            IBus bus)
        {
            _context = context;
            _bus = bus;
        }

        public override async Task<UseCaseResponse<IGetAllSellsResultContract>> Ask
            (IGetAllSellsRequestContract input, CancellationToken cancellationToken)
        {
            var sells = (await _context
                .Sells
                .Where(c => c.InstanceId == input.InstanceId)
                .ToListAsync(cancellationToken))
                .Select(a =>
                {
                    var r = new SelleViewModel
                    {
                        Id = a.Id,
                        Number = a.Number,
                        Date = a.Date,
                        Cash = a.Details
                            .Select(f => f.Price * f.Count)
                            .Sum()
                    };
                    var getUnitStringByIdRequestContract = 
                        new MasstransitUseCaseRequest<IGetUnitStringByIdRequestContract, IGetUnitStringByIdResultContract>(_bus, new GetUnitStringByIdRequestContract() {UnitId = a.Details.First().FromUnitId}).GetResponse().Result;
                    r.Unit = getUnitStringByIdRequestContract.IsSuccess() ? getUnitStringByIdRequestContract.Success.Data : throw new Exception();

                    return r;
                })
                .ToList();

            return new UseCaseResponse<IGetAllSellsResultContract>(new GetAllSellsResultContract() {Data = sells});
        }
    }   
}