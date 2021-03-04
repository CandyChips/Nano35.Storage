using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllWarehousesOfUnit
{
    public class GetAllWarehousesOfUnitRequest :
        IPipelineNode<
            IGetAllWarehousesOfUnitRequestContract,
            IGetAllWarehousesOfUnitResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllWarehousesOfUnitRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetAllWarehousesOfUnitSuccessResultContract : 
            IGetAllWarehousesOfUnitSuccessResultContract
        {
            public IEnumerable<IWarehouseOfUnitViewModel> Data { get; set; }
        }
        
        public async Task<IGetAllWarehousesOfUnitResultContract> Ask
            (IGetAllWarehousesOfUnitRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = await _context
                .Warehouses
                .Where(c => c.UnitId == input.UnitId)
                .MapAllToAsync<IWarehouseOfUnitViewModel>();

            return new GetAllWarehousesOfUnitSuccessResultContract() {Data = result};
        }
    }   
}