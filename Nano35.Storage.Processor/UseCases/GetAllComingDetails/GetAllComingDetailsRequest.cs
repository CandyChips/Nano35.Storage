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
        UseCaseEndPointNodeBase<IGetAllComingDetailsRequestContract, IGetAllComingDetailsResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllComingDetailsRequest(ApplicationContext context)
        {
            _context = context;
        }

        public override async Task<UseCaseResponse<IGetAllComingDetailsResultContract>> Ask(
            IGetAllComingDetailsRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = 
                (await _context
                .Comings
                .FirstOrDefaultAsync(f => f.Id == input.ComingId, cancellationToken: cancellationToken))
                .Details
                .Select(a => 
                    new ComingDetailViewModel()
                        {StorageItemId = a.StorageItemId,
                         Count = a.Count,
                         Price = a.Price,
                         PlaceOnStorage = a.ToWarehouse.ToString(),
                         StorageItem = a.ToWarehouse.StorageItem.ToString()})
                .ToList();
            return new UseCaseResponse<IGetAllComingDetailsResultContract>(new GetAllComingDetailsResultContract()
                {Data = result});
        }
    }   
}