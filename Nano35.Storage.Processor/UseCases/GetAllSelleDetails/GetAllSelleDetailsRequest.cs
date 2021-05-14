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

namespace Nano35.Storage.Processor.UseCases.GetAllSelleDetails
{
    public class GetAllSelleDetailsRequest :
        UseCaseEndPointNodeBase<IGetAllSelleDetailsRequestContract, IGetAllSelleDetailsResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;
        public GetAllSelleDetailsRequest(ApplicationContext context, IBus bus) { _context = context; _bus = bus; }
        public override async Task<UseCaseResponse<IGetAllSelleDetailsResultContract>>Ask(
            IGetAllSelleDetailsRequestContract input, 
            CancellationToken cancellationToken)
        {
            var selle = await _context
                .Sells
                .FirstOrDefaultAsync(c => c.Id == input.SelleId, cancellationToken: cancellationToken);
            var details = selle.Details
                .Select(a =>
                    new SelleDetailViewModel()
                    {
                        StorageItem = _context.StorageItems.FirstOrDefault(e => a.StorageItemId == e.Id)?.ToString(), 
                        Count = a.Count,
                        Price = a.Price, 
                        PlaceOnStorage = a.FromPlace, 
                        StorageItemId = a.StorageItemId
                    })
                .ToList();


            return new UseCaseResponse<IGetAllSelleDetailsResultContract>(new GetAllSelleDetailsResultContract() {Data = details});
        }
    }   
}