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
using Nano35.Storage.Processor.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.GetAllSells
{
    public class GetAllSellsRequest :
        IPipelineNode<
            IGetAllSellsRequestContract,
            IGetAllSellsResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetAllSellsRequest(
            ApplicationContext context, 
            IBus bus)
        {
            _context = context;
            _bus = bus;
        }
        
        private class GetAllSellsSuccessResultContract : 
            IGetAllSellsSuccessResultContract
        {
            public IEnumerable<ISelleViewModel> Data { get; set; }
        }
        
        private class ComingImpl : ISelleViewModel
        {
            public Guid Id { get; set; }
            public string Number { get; set; }
            public DateTime Date { get; set; }
            public string Unit { get; set; }
            public string Client { get; set; }
            public double Cash { get; set; }
        }

        public async Task<IGetAllSellsResultContract> Ask
            (IGetAllSellsRequestContract input, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }   
}