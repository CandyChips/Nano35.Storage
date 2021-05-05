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

namespace Nano35.Storage.Processor.UseCases.GetAllMoves
{
    public class GetAllMovesRequest :
        UseCaseEndPointNodeBase<IGetAllMovesRequestContract, IGetAllMovesResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetAllMovesRequest(
            ApplicationContext context, 
            IBus bus)
        {
            _context = context;
            _bus = bus;
        }
        
        public override async Task<UseCaseResponse<IGetAllMovesResultContract>> Ask
            (IGetAllMovesRequestContract input, CancellationToken cancellationToken)
        {
            var moves = await _context
                .Moves
                .Where(c => c.InstanceId == input.InstanceId)
                .ToListAsync(cancellationToken);
            
            var result = moves
                .Select(a =>
                {
                    var r = new MoveViewModel()
                    {
                        Id = a.Id,
                        Number = a.Number,
                        Date = a.Date
                    };
                    var getToUnitStringByIdRequestContract = 
                        new MasstransitUseCaseRequest<IGetUnitStringByIdRequestContract, IGetUnitStringByIdResultContract>(_bus, new GetUnitStringByIdRequestContract() {UnitId = _context.MoveDetails.FirstOrDefault(h => h.MoveId == a.Id).FromUnitId}).GetResponse().Result;
                    r.ToUnit = getToUnitStringByIdRequestContract.IsSuccess() ? getToUnitStringByIdRequestContract.Success.Data : throw new Exception();
                    var getFromUnitStringByIdRequestContract = 
                        new MasstransitUseCaseRequest<IGetUnitStringByIdRequestContract, IGetUnitStringByIdResultContract>(_bus, new GetUnitStringByIdRequestContract() {UnitId = _context.MoveDetails.FirstOrDefault(h => h.MoveId == a.Id).ToUnitId}).GetResponse().Result;
                    r.FromUnit = getFromUnitStringByIdRequestContract.IsSuccess() ? getFromUnitStringByIdRequestContract.Success.Data : throw new Exception();

                    return r;
                })
                .ToList();

            return new UseCaseResponse<IGetAllMovesResultContract>(new GetAllMovesResultContract() {Data = result});
        }
    }   
}