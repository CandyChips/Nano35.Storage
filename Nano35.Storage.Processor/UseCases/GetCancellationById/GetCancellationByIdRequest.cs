﻿using System;
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

namespace Nano35.Storage.Processor.UseCases.GetCancellationById
{
    public class GetCancellationByIdRequest :
        EndPointNodeBase<IGetCancellationByIdRequestContract, IGetCancellationByIdResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetCancellationByIdRequest(
            ApplicationContext context, IBus bus)
        {
            _context = context;
            _bus = bus;
        }
        
        public override async Task<IGetCancellationByIdResultContract> Ask(
            IGetCancellationByIdRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = await _context.Cancellations
                .FirstAsync(c => c.Id == input.Id, cancellationToken: cancellationToken);

            var cancellation = new CancellationViewModel()
            {
                Id = result.Id,
                Comment = result.Comment,
                Date = result.Date,
                Number = result.Number
            };
            
            var getUnitStringRequest = new GetUnitStringById(_bus,
                new GetUnitStringByIdRequestContract() {UnitId = result.Details.First().FromUnitId});
            cancellation.Unit = getUnitStringRequest.GetResponse().Result switch
            {
                IGetUnitStringByIdSuccessResultContract success => success.Data,
                _ => throw new Exception()
            };

            return new GetCancellationByIdSuccessResultContract() {Cancellation = cancellation};
        }
    }   
}