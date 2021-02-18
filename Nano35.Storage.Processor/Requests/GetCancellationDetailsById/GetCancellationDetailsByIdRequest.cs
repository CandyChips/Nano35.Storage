﻿using System;
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

namespace Nano35.Storage.Processor.Requests.GetCancellationDetailsById
{
    public class GetCancellationDetailsByIdRequest :
        IPipelineNode<
            IGetCancellationDetailsByIdRequestContract,
            IGetCancellationDetailsByIdResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetCancellationDetailsByIdRequest(
            ApplicationContext context, 
            IBus bus)
        {
            _context = context;
            _bus = bus;
        }
        
        private class GetCancellationDetailsByIdSuccessResultContract : 
            IGetCancellationDetailsByIdSuccessResultContract
        {
            public Guid Id { get; set; }
            public string Number { get; set; }
            public DateTime Date { get; set; }
            public IUnitViewModel Unit { get; set; }
            public IClientViewModel Client { get; set; }
            public IEnumerable<ICancellationDetailViewModel> Details { get; set; }
        }
        
        private class ComingDetailImpl : ICancellationDetailViewModel
        {
            public int Count { get; set; }
            public string PlaceOnStorage { get; set; }
            public IStorageItemViewModel StorageItem { get; set; }
        }

        public async Task<IGetCancellationDetailsByIdResultContract> Ask
            (IGetCancellationDetailsByIdRequestContract input, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            
            return new GetCancellationDetailsByIdSuccessResultContract() {Data = result};
        }
    }   
}