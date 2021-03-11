using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllPlacesOnStorage
{
    public class GetAllPlacesOnStorageValidatorErrorResult :
        IGetAllPlacesOnStorageErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllPlacesOnStorageRequest:
        IPipelineNode<
            IGetAllPlacesOnStorageContract, 
            IGetAllPlacesOnStorageResultContract>
    {
        private readonly IPipelineNode<
            IGetAllPlacesOnStorageContract, 
            IGetAllPlacesOnStorageResultContract> _nextNode;

        public ValidatedGetAllPlacesOnStorageRequest(
            IPipelineNode<
                IGetAllPlacesOnStorageContract,
                IGetAllPlacesOnStorageResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllPlacesOnStorageResultContract> Ask(
            IGetAllPlacesOnStorageContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllPlacesOnStorageValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}