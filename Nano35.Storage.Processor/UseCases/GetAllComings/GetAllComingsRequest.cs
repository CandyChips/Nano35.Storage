using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllComings
{
    public class GetAllComingsRequest :
        IPipelineNode<
            IGetAllComingsRequestContract,
            IGetAllComingsResultContract>
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
        
        private class GetAllComingsSuccessResultContract : 
            IGetAllComingsSuccessResultContract
        {
            public IEnumerable<IComingViewModel> Data { get; set; }
        }
        
        private class ComingImpl : IComingViewModel
        {
            public Guid Id { get; set; }
            public string Number { get; set; }
            public DateTime Date { get; set; }
            public string Unit { get; set; }
            public string Client { get; set; }
            public double Cash { get; set; }
        }

        public async Task<IGetAllComingsResultContract> Ask
            (IGetAllComingsRequestContract input, CancellationToken cancellationToken)
        {
            var result = await _context
                .Comings
                .Where(c => c.InstanceId == input.InstanceId)
                .Select(a =>
                    new ComingImpl()
                    {
                        Id = a.Id,
                        Number = a.Number,
                        Date = a.Date,
                        Cash = a.Details
                            .Select(f => f.Price * f.Count)
                            .Sum()
                    })
                .ToListAsync(cancellationToken);
            
            return new GetAllComingsSuccessResultContract() {Data = result};
        }
    }   
}