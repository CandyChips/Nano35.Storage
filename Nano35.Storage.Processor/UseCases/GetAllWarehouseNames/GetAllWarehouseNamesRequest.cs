using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllWarehouseNames
{
    public class GetAllWarehouseNamesRequest :
        IPipelineNode<
            IGetAllWarehouseNamesRequestContract,
            IGetAllWarehouseNamesResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetAllWarehouseNamesRequest(
            ApplicationContext context, 
            IBus bus)
        {
            _context = context;
            _bus = bus;
        }

        public async Task<IGetAllWarehouseNamesResultContract> Ask(
            IGetAllWarehouseNamesRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = await _context.Warehouses
                .Where(x => 
                    x.UnitId == input.UnitId &&
                    x.StorageItemId == input.StorageItemId &&
                    x.IsDeleted == false &&
                    x.Count > 0)
                .Select(a => new WarehouseViewModel()
                {
                    UnitId = a.UnitId,
                    StorageItemId = a.StorageItemId,
                    Name = a.Name,
                    Count = a.Count
                })
                .ToListAsync(cancellationToken: cancellationToken);
                    
            return new GetAllWarehouseNamesSuccessResultContract() {Data = result};
        }
    }   
}