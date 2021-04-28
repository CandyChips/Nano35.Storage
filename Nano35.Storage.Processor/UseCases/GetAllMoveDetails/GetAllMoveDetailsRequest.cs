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

namespace Nano35.Storage.Processor.UseCases.GetAllMoveDetails
{
    public class GetAllMoveDetailsRequest :
        EndPointNodeBase<IGetAllMoveDetailsRequestContract, IGetAllMoveDetailsResultContract>
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

        public override async Task<IGetAllMoveDetailsResultContract> Ask(
            IGetAllMoveDetailsRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = await _context
                .MoveDetails
                .Where(f => f.MoveId == input.MoveId)
                .Select(a => new MoveDetailViewModel()
                    {StorageItemId = a.StorageItemId,
                     FromPlaceOnStorage = a.FromWarehouse.ToString(),
                     ToPlaceOnStorage = a.ToWarehouse.ToString(),
                     Count = a.Count,
                     StorageItem = a.FromWarehouse.StorageItem.ToString()})
                .ToListAsync(cancellationToken: cancellationToken);

            return new GetAllMoveDetailsSuccessResultContract() { Data = result };
        }
    }   
}