using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllWarehousesOfItem
{
    public class GetAllWarehousesOfItemRequest :
        IPipelineNode<
            IGetAllWarehousesOfItemRequestContract,
            IGetAllWarehousesOfItemResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllWarehousesOfItemRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetAllWarehousesOfItemSuccessResultContract : 
            IGetAllWarehousesOfItemSuccessResultContract
        {
            public List<IWarehouseOfItemViewModel> Data { get; set; }
        }
        
        public async Task<IGetAllWarehousesOfItemResultContract> Ask
            (IGetAllWarehousesOfItemRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = await _context
                .Warehouses
                .Where(c => c.StorageItemId == input.StorageItemId)
                .MapAllToAsync<IWarehouseOfItemViewModel>();

            return new GetAllWarehousesOfItemSuccessResultContract() {Data = result};
        }
    }   
}