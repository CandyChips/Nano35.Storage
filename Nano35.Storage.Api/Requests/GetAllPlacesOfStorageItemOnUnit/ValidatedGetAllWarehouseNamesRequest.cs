using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllPlacesOfStorageItemOnUnit
{
    public class ValidatedGetAllPlacesOfStorageItemOnUnitRequest:
        PipeNodeBase<
            IGetAllPlacesOfStorageItemOnUnitRequestContract, 
            IGetAllPlacesOfStorageItemOnUnitResultContract>
    {

        public ValidatedGetAllPlacesOfStorageItemOnUnitRequest(
            IPipeNode<IGetAllPlacesOfStorageItemOnUnitRequestContract,
                IGetAllPlacesOfStorageItemOnUnitResultContract> next) : base(next)
        {}

        public override async Task<IGetAllPlacesOfStorageItemOnUnitResultContract> Ask(
            IGetAllPlacesOfStorageItemOnUnitRequestContract input)
        {
            return await DoNext(input);
        }
    }
}