using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetStorageItemById
{
    public class GetStorageItemByIdValidatorErrorResult :
        IGetStorageItemByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetStorageItemByIdRequest:
        PipeNodeBase<
            IGetStorageItemByIdRequestContract, 
            IGetStorageItemByIdResultContract>
    {
        public ValidatedGetStorageItemByIdRequest(
            IPipeNode<IGetStorageItemByIdRequestContract,
                IGetStorageItemByIdResultContract> next) : base(next)
        {}

        public override async Task<IGetStorageItemByIdResultContract> Ask(
            IGetStorageItemByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetStorageItemByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}