﻿using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllStorageItemConditions
{
    public class GetAllStorageItemConditionsValidatorErrorResult :
        IGetAllStorageItemConditionsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllStorageItemConditionsRequest:
        IPipelineNode<
            IGetAllStorageItemConditionsRequestContract, 
            IGetAllStorageItemConditionsResultContract>
    {
        private readonly IPipelineNode<
            IGetAllStorageItemConditionsRequestContract, 
            IGetAllStorageItemConditionsResultContract> _nextNode;

        public ValidatedGetAllStorageItemConditionsRequest(
            IPipelineNode<
                IGetAllStorageItemConditionsRequestContract,
                IGetAllStorageItemConditionsResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllStorageItemConditionsResultContract> Ask(
            IGetAllStorageItemConditionsRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllStorageItemConditionsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}