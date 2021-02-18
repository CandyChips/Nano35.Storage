using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.GetSelleDetailsById
{
    public class GetSelleDetailsByIdRequest :
        IPipelineNode<
            IGetSelleDetailsByIdRequestContract,
            IGetSelleDetailsByIdResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetSelleDetailsByIdRequest(
            ApplicationContext context, 
            IBus bus)
        {
            _context = context;
            _bus = bus;
        }
        
        private class GetSelleDetailsByIdSuccessResultContract : 
            IGetSelleDetailsByIdSuccessResultContract
        {
            Guid Id { get; set; }
            string Number { get; set; }
            DateTime Date { get; set; }
            IUnitViewModel Unit { get; set; }
            IClientViewModel Client { get; set; }
            IEnumerable<ISelleDetailViewModel> Details { get; set; }
        }
        
        private class ComingImpl : ISelleDetailViewModel
        {
            public Guid Id { get; set; }
            public string Number { get; set; }
            public DateTime Date { get; set; }
            public string Unit { get; set; }
            public string Client { get; set; }
            public double Cash { get; set; }
        }

        public async Task<IGetSelleDetailsByIdResultContract> Ask
            (IGetSelleDetailsByIdRequestContract input, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            
            return new GetSelleDetailsByIdSuccessResultContract() {Data = result};
        }
    }   
}