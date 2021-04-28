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
using Nano35.Storage.Processor.Requests;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllComings
{
    public class GetAllComingsRequest :
        EndPointNodeBase<IGetAllComingsRequestContract, IGetAllComingsResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetAllComingsRequest(
            ApplicationContext context, 
            IBus bus)
        {
            _context = context;
            _bus = bus;
        }
        
        public override async Task<IGetAllComingsResultContract> Ask
            (IGetAllComingsRequestContract input, CancellationToken cancellationToken)
        {
            var result = (await _context
                .Comings
                .Where(c => c.InstanceId == input.InstanceId)
                .ToListAsync(cancellationToken))
                .Select(a =>
                    new ComingViewModel
                        {Id = a.Id,
                         Number = a.Number,
                         Date = a.Date,
                         Cash = a.Details
                             .Select(f => f.Price * f.Count)
                             .Sum(),
                         Unit = new GetUnitStringById(_bus,
                                     new GetUnitStringByIdRequestContract() {UnitId = a.Details.First().ToUnitId})
                                 .GetResponse().Result switch
                             {
                                 IGetUnitStringByIdSuccessResultContract success => success.Data,
                                 _ => throw new Exception()
                             },
                         Client = new GetClientStringById(_bus,
                                     new GetClientStringByIdRequestContract() {ClientId = a.ClientId}).GetResponse()
                                 .Result switch
                             {
                                 IGetClientStringByIdSuccessResultContract success => success.Data,
                                 _ => throw new Exception()
                             }})
                .ToList();
            
            return new GetAllComingsSuccessResultContract() {Data = result};
        }
    }   
}