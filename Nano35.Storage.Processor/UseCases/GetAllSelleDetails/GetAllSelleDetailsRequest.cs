﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllSelleDetails
{
    public class GetAllSelleDetailsRequest :
        UseCaseEndPointNodeBase<IGetAllSelleDetailsRequestContract, IGetAllSelleDetailsResultContract>
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

        public override async Task<UseCaseResponse<IGetAllSelleDetailsResultContract>>Ask
            (IGetAllSelleDetailsRequestContract input, CancellationToken cancellationToken)
        {
            var selleDetails = await _context
                .SelleDetails
                .Where(c => c.SelleId == input.SelleId)
                .ToListAsync(cancellationToken);
            var result    = selleDetails
                .Select(a =>
                {
                    var r = new SelleDetailViewModel()
                    {
                        StorageItemId = a.StorageItemId,
                        Count = a.Count,
                        PlaceOnStorage = a.FromPlace,
                        Price = a.Price
                    };
                    var getStorageItemByIdRequestContract =
                        new MasstransitUseCaseRequest<IGetStorageItemByIdRequestContract,
                            IGetStorageItemByIdResultContract>(_bus,
                            new GetStorageItemByIdRequestContract() {Id = a.StorageItemId}).GetResponse().Result;
                    r.StorageItem = getStorageItemByIdRequestContract.IsSuccess()
                        ? getStorageItemByIdRequestContract.Success.Data.Article
                        : throw new Exception();

                    return r;
                })
                .ToList();

            return new UseCaseResponse<IGetAllSelleDetailsResultContract>(new GetAllSelleDetailsResultContract()
                {Data = result});
        }
    }   
}