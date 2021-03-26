﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllSelleDetails
{
    public class GetAllSelleDetailsRequest :
        IPipelineNode<
            IGetAllSelleDetailsRequestContract,
            IGetAllSelleDetailsResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetAllSelleDetailsRequest(
            ApplicationContext context, 
            IBus bus)
        {
            _context = context;
            _bus = bus;
        }
        
        private class GetAllSelleDetailsSuccessResultContract : 
            IGetAllSelleDetailsSuccessResultContract
        {
            public List<SelleDetailViewModel> Data { get; set; }
        }

        public async Task<IGetAllSelleDetailsResultContract> Ask
            (IGetAllSelleDetailsRequestContract input, CancellationToken cancellationToken)
        {
            var result = await _context
                .Sells
                .Where(c => c.InstanceId == input.SelleId)
                .Select(a =>
                    new SelleDetailViewModel()
                    {
                    })
                .ToListAsync(cancellationToken: cancellationToken);

            return new GetAllSelleDetailsSuccessResultContract() {Data = result};
        }
    }   
}