using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
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
            public IEnumerable<IMoveViewModel> Data { get; set; }
        }
        
        private class MoveImpl : IMoveViewModel
        {
            public Guid Id { get; set; }
            public string Number { get; set; }
            public DateTime Date { get; set; }
            public string Unit { get; set; }
            public string Client { get; set; }
            public double Cash { get; set; }
        }

        public async Task<IGetAllMovesResultContract> Ask
            (IGetAllMovesRequestContract input, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }   
}