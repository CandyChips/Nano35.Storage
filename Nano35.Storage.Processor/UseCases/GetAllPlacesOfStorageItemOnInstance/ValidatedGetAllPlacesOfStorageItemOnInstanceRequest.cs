using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllPlacesOfStorageItemOnInstance
{
    public class GetAllPlacesOfStorageItemOnInstanceValidatorErrorResult :
        IGetAllPlacesOfStorageItemOnInstanceErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllPlacesOfStorageItemOnInstanceRequest:
        PipeNodeBase<
            IGetAllPlacesOfStorageItemOnInstanceContract, 
            IGetAllPlacesOfStorageItemOnInstanceResultContract>
    {
        public ValidatedGetAllPlacesOfStorageItemOnInstanceRequest(
            IPipeNode<IGetAllPlacesOfStorageItemOnInstanceContract,
                IGetAllPlacesOfStorageItemOnInstanceResultContract> next) : base(next)
        { }

        public override async Task<IGetAllPlacesOfStorageItemOnInstanceResultContract> Ask(
            IGetAllPlacesOfStorageItemOnInstanceContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllPlacesOfStorageItemOnInstanceValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}