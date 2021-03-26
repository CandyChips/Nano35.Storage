﻿using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllMoveDetails
{
    public class GetAllMoveDetailsValidatorErrorResult : 
        IGetAllMoveDetailsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllMoveDetailsRequest:
        PipeNodeBase<IGetAllMoveDetailsRequestContract, IGetAllMoveDetailsResultContract>
    {
        public ValidatedGetAllMoveDetailsRequest(
            IPipeNode<IGetAllMoveDetailsRequestContract, IGetAllMoveDetailsResultContract> next) :
            base(next) { }

        public override async Task<IGetAllMoveDetailsResultContract> Ask(
            IGetAllMoveDetailsRequestContract input)
        {
            if (false)
            {
                return new GetAllMoveDetailsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}