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

namespace Nano35.Storage.Processor.UseCases.GetAllCancellations
{
    public class GetAllCancellationsRequest :
        UseCaseEndPointNodeBase<IGetAllCancellationsRequestContract, IGetAllCancellationsResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetAllCancellationsRequest(
            ApplicationContext context, IBus bus)
        {
            _context = context;
            _bus = bus;
        }
        
        public override async Task<UseCaseResponse<IGetAllCancellationsResultContract>> Ask(
            IGetAllCancellationsRequestContract input, 
            CancellationToken cancellationToken)
        {
            var queue = await _context
                .Cancellations
                .Where(w => w.InstanceId == input.InstanceId)
                .ToListAsync(cancellationToken);


            var result = queue
                .Select(a =>
                    new CancellationViewModel()
                        {Id = a.Id,
                         Number = a.Number,
                         Date = a.Date,
                         Comment = a.Comment,
                         Unit = 
                             new GetUnitStringById(_bus, new GetUnitStringByIdRequestContract() {UnitId = input.UnitId}).GetResponse().Result switch
                             {
                                 IGetUnitStringByIdSuccessResultContract success => success.Data,
                                 _ => throw new Exception()
                             }})
                .ToList();

            return new UseCaseResponse<IGetAllCancellationsResultContract>(new GetAllCancellationsResultContract(){Data = result});
        }
    }   
}