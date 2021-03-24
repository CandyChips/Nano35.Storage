using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllMoves
{
    public class GetAllMovesRequest :
        IPipelineNode<
            IGetAllMovesRequestContract,
            IGetAllMovesResultContract>
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
        
        private class GetAllMovesSuccessResultContract : 
            IGetAllMovesSuccessResultContract
        {
            public List<MoveViewModel> Data { get; set; }
        }

        public async Task<IGetAllMovesResultContract> Ask
            (IGetAllMovesRequestContract input, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }   
}