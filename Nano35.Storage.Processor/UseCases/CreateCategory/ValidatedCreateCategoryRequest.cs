﻿using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.CreateCategory
{
    public class CreateCategoryValidatorErrorResult : ICreateCategoryErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateCategoryRequest:
        PipeNodeBase<
            ICreateCategoryRequestContract,
            ICreateCategoryResultContract>
    {
        public ValidatedCreateCategoryRequest(
            IPipeNode<ICreateCategoryRequestContract,
                ICreateCategoryResultContract> next) : base(next)
        { }

        public override async Task<ICreateCategoryResultContract> Ask(
            ICreateCategoryRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new CreateCategoryValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}