using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllMoveDetails
{
    public class GetAllMoveDetailsRequest :
        IPipelineNode<
            IGetAllMoveDetailsRequestContract,
            IGetAllMoveDetailsResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetAllMoveDetailsRequest(
            ApplicationContext context, 
            IBus bus)
        {
            _context = context;
            _bus = bus;
        }

        public async Task<IGetAllMoveDetailsResultContract> Ask(
            IGetAllMoveDetailsRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = _context
                .Moves
                .FirstOrDefault(f => f.Id == input.MoveId)
                ?.Details
                .Select(a => new MoveDetailViewModel()
                {
                    FromPlaceOnStorage = a.FromWarehouse.ToString(),
                    ToPlaceOnStorage = a.ToWarehouse.ToString(),
                    Count = a.Count,
                    StorageItem = a.FromWarehouse.StorageItem.ToString()
                })
                .ToList();

            return new GetAllMoveDetailsSuccessResultContract() {Data = result};
        }
    }   
}