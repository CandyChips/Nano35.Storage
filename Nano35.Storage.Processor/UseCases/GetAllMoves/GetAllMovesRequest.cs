using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllMoves
{
    public class GetAllMovesRequest :
        EndPointNodeBase<IGetAllMovesRequestContract, IGetAllMovesResultContract>
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
        
        public override async Task<IGetAllMovesResultContract> Ask
            (IGetAllMovesRequestContract input, CancellationToken cancellationToken)
        {
            var result = await _context
                .Moves
                .Where(w => w.InstanceId == input.InstanceId)
                .Select(a => new MoveViewModel() {})
                .ToListAsync(cancellationToken: cancellationToken);

            return new GetAllMovesSuccessResultContract() {Data = result};
        }
    }   
}