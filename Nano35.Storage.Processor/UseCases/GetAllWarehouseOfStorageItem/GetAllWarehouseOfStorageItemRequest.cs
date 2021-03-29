using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllWarehouseOfStorageItem
{
    public class GetAllWarehouseOfStorageItemRequest :
        IPipelineNode<
            IGetAllWarehouseOfStorageItemRequestContract,
            IGetAllWarehouseOfStorageItemResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetAllWarehouseOfStorageItemRequest(
            ApplicationContext context, 
            IBus bus)
        {
            _context = context;
            _bus = bus;
        }

        public async Task<IGetAllWarehouseOfStorageItemResultContract> Ask(
            IGetAllWarehouseOfStorageItemRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = await _context.Warehouses
                .Where(x => 
                    x.StorageItemId == input.StorageItemId &&
                    x.IsDeleted == false &&
                    x.Count > 0)
                .Select(a => new PlaceOnStorage()
                {
                    Name = a.Name,
                    Count = a.Count,
                    UnitId = a.UnitId,
                    StorageItemId = a.StorageItemId
                })
                .ToListAsync(cancellationToken);
                    
            return new GetAllWarehouseOfStorageItemSuccessResultContract() {Data = result};
        }
    }   
}