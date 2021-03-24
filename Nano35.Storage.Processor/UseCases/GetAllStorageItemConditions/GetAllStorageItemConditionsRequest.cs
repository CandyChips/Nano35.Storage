﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllStorageItemConditions
{
    public class GetAllStorageItemConditionsRequest :
        IPipelineNode<
            IGetAllStorageItemConditionsRequestContract,
            IGetAllStorageItemConditionsResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllStorageItemConditionsRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetAllStorageItemConditionsSuccessResultContract : 
            IGetAllStorageItemConditionsSuccessResultContract
        {
            public List<StorageItemConditionViewModel> Data { get; set; }
        }
        
        public async Task<IGetAllStorageItemConditionsResultContract> Ask
            (IGetAllStorageItemConditionsRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = await _context
                .StorageItemConditions
                .MapAllToAsync<StorageItemConditionViewModel>();

            return new GetAllStorageItemConditionsSuccessResultContract() {Data = result};
        }
    }   
}