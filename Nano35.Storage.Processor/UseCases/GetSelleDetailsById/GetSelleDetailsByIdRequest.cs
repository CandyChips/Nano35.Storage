using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Models;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetSelleDetailsById
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
            public Guid Id { get; set; }
            public string Number { get; set; }
            public DateTime Date { get; set; }
            public IUnitViewModel Unit { get; set; }
            public IClientViewModel Client { get; set; }
            public IEnumerable<ISelleDetailViewModel> Details { get; set; }
        }
        
        private class ComingImpl : ISelleDetailViewModel
        {
            public int Count { get; set; }
            public string PlaceOnStorage { get; set; }
            public string StorageItem { get; set; }
            public double Price { get; set; }
        }

        public async Task<IGetSelleDetailsByIdResultContract> Ask
            (IGetSelleDetailsByIdRequestContract input, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }   
}