using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllPlacesOnStorage
{
    public class GetAllPlacesOnStorageRequest :
        IPipelineNode<
            IGetAllPlacesOnStorageContract,
            IGetAllPlacesOnStorageResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllPlacesOnStorageRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetAllPlacesOnStorageSuccessResultContract : 
            IGetAllPlacesOnStorageSuccessResultContract
        {
            public List<IPlaceOnStorage> Data { get; set; }
        }
        
        public async Task<IGetAllPlacesOnStorageResultContract> Ask
            (IGetAllPlacesOnStorageContract input, 
            CancellationToken cancellationToken)
        {
            var result = await _context
                .Warehouses
                .Where(c => c.UnitId == input.UnitId && c.StorageItemId == input.StorageItemId)
                .MapAllToAsync<IPlaceOnStorage>();

            return new GetAllPlacesOnStorageSuccessResultContract() {Data = result};
        }
    }   
}