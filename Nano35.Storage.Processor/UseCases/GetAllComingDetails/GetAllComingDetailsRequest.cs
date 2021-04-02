using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllComingDetails
{
    public class GetAllComingDetailsRequest :
        EndPointNodeBase<IGetAllComingDetailsRequestContract, IGetAllComingDetailsResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetAllComingDetailsRequest(
            ApplicationContext context, 
            IBus bus)
        {
            _context = context;
            _bus = bus;
        }

        public override async Task<IGetAllComingDetailsResultContract> Ask(
            IGetAllComingDetailsRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = (await _context
                    .Comings
                    .FirstOrDefaultAsync(
                        f => f.Id == input.ComingId,
                        cancellationToken: cancellationToken))
                .Details
                .Select(a => new ComingDetailViewModel()
                {
                    Count = a.Count,
                    Price = a.Price,
                    PlaceOnStorage = a.ToWarehouse.ToString(),
                    StorageItem = a.ToWarehouse.StorageItem.ToString()
                })
                .ToList();
            
                    
            return new GetAllComingDetailsSuccessResultContract() {Data = result};
        }
    }   
}