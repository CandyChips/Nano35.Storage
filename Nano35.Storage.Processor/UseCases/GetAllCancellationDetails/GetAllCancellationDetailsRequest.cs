using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllCancellationDetails
{
    public class GetAllCancellationDetailsRequest :
        EndPointNodeBase<IGetAllCancellationDetailsRequestContract, IGetAllCancellationDetailsResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllCancellationDetailsRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        public override async Task<IGetAllCancellationDetailsResultContract> Ask(
            IGetAllCancellationDetailsRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = 
                (await _context
                .Cancellations
                .FirstAsync(c => c.Id == input.CancellationId, cancellationToken: cancellationToken))
                .Details
                .Select(a =>
                    new CancellationDetailViewModel()
                        {StorageItemId = a.FromWarehouse.StorageItemId,
                         Count = a.Count,
                         StorageItem = a.FromWarehouse.StorageItem.ToString(), 
                         PlaceOnStorage = a.FromPlace.ToString()})
                .ToList();
            return new GetAllCancellationDetailsSuccessResultContract() {Data = result};
        }
    }   
}