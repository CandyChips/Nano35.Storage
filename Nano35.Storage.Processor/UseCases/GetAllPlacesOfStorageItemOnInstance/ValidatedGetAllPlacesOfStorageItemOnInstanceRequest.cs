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
        IPipelineNode<
            IGetAllPlacesOfStorageItemOnInstanceContract, 
            IGetAllPlacesOfStorageItemOnInstanceResultContract>
    {
        private readonly IPipelineNode<
            IGetAllPlacesOfStorageItemOnInstanceContract, 
            IGetAllPlacesOfStorageItemOnInstanceResultContract> _nextNode;

        public ValidatedGetAllPlacesOfStorageItemOnInstanceRequest(
            IPipelineNode<
                IGetAllPlacesOfStorageItemOnInstanceContract,
                IGetAllPlacesOfStorageItemOnInstanceResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllPlacesOfStorageItemOnInstanceResultContract> Ask(
            IGetAllPlacesOfStorageItemOnInstanceContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllPlacesOfStorageItemOnInstanceValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}