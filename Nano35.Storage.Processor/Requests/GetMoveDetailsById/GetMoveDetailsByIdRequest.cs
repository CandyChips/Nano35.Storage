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

namespace Nano35.Storage.Processor.Requests.GetMoveDetailsById
{
    public class GetMoveDetailsByIdRequest :
        IPipelineNode<
            IGetMoveDetailsByIdRequestContract,
            IGetMoveDetailsByIdResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetMoveDetailsByIdRequest(
            ApplicationContext context, 
            IBus bus)
        {
            _context = context;
            _bus = bus;
        }
        
        private class GetMoveDetailsByIdSuccessResultContract : 
            IGetMoveDetailsByIdSuccessResultContract
        {
            public Guid Id { get; set; }
            public string Number { get; set; }
            public DateTime Date { get; set; }
            public IUnitViewModel Unit { get; set; }
            public IClientViewModel Client { get; set; }
            public IEnumerable<IMoveDetailViewModel> Details { get; set; }
        }
        
        private class ComingImpl : IMoveDetailViewModel
        {
            public int Count { get; set; }
            public string PlaceOnStorage { get; set; }
            public string StorageItem { get; set; }
            public double Price { get; set; }
        }

        public async Task<IGetMoveDetailsByIdResultContract> Ask
            (IGetMoveDetailsByIdRequestContract input, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }   
}