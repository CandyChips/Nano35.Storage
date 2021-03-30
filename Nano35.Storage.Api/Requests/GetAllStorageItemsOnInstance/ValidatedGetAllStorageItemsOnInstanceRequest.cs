﻿using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllStorageItemsOnInstance
{
    public class GetAllStorageItemsOnInstanceValidatorErrorResult : 
        IGetAllStorageItemsOnInstanceErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllStorageItemsOnInstanceRequest:
        PipeNodeBase<
            IGetAllStorageItemsOnInstanceContract, 
            IGetAllStorageItemsOnInstanceResultContract>
    {

        public ValidatedGetAllStorageItemsOnInstanceRequest(
            IPipeNode<IGetAllStorageItemsOnInstanceContract,
                IGetAllStorageItemsOnInstanceResultContract> next) : base(next)
        { }

        public override async Task<IGetAllStorageItemsOnInstanceResultContract> Ask(
            IGetAllStorageItemsOnInstanceContract input)
        {
            if (false)
            {
                return new GetAllStorageItemsOnInstanceValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}