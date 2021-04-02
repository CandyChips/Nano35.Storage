﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetStorageItemById
{
    public class GetStorageItemByIdRequest :
        EndPointNodeBase<IGetStorageItemByIdRequestContract, IGetStorageItemByIdResultContract>
    {
        private readonly ApplicationContext _context;

        public GetStorageItemByIdRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        public override async Task<IGetStorageItemByIdResultContract> Ask(
            IGetStorageItemByIdRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = (await _context.StorageItems
                .FirstOrDefaultAsync(c => c.Id == input.Id, cancellationToken: cancellationToken))
                .MapTo<StorageItemViewModel>();

            return new GetStorageItemByIdSuccessResultContract() {Data = result};
        }
    }   
}