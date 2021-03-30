using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllPlacesOfStorageItemOnUnit
{
    public class GetAllPlacesOfStorageItemOnUnitValidatorErrorResult :
        IGetAllPlacesOfStorageItemOnUnitErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllPlacesOfStorageItemOnUnitRequest:
        IPipelineNode<
            IGetAllPlacesOfStorageItemOnUnitRequestContract, 
            IGetAllPlacesOfStorageItemOnUnitResultContract>
    {
        private readonly IPipelineNode<
            IGetAllPlacesOfStorageItemOnUnitRequestContract, 
            IGetAllPlacesOfStorageItemOnUnitResultContract> _nextNode;

        public ValidatedGetAllPlacesOfStorageItemOnUnitRequest(
            IPipelineNode<
                IGetAllPlacesOfStorageItemOnUnitRequestContract,
                IGetAllPlacesOfStorageItemOnUnitResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllPlacesOfStorageItemOnUnitResultContract> Ask(
            IGetAllPlacesOfStorageItemOnUnitRequestContract input,
            CancellationToken cancellationToken)
        {
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}