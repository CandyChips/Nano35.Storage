using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllPlacesOfStorageItemOnUnit
{
    public class GetAllPlacesOfStorageItemOnUnitRequest :
        EndPointNodeBase<
            IGetAllPlacesOfStorageItemOnUnitRequestContract,
            IGetAllPlacesOfStorageItemOnUnitResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetAllPlacesOfStorageItemOnUnitRequest(
            ApplicationContext context, 
            IBus bus)
        {
            _context = context;
            _bus = bus;
        }

        public override async Task<IGetAllPlacesOfStorageItemOnUnitResultContract> Ask(
            IGetAllPlacesOfStorageItemOnUnitRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = await _context.Warehouses
                .Where(x => 
                    x.StorageItemId == input.StorageItemId &&
                    x.UnitId == input.UnitContainsStorageItemId &&
                    x.IsDeleted == false &&
                    x.Count > 0)
                .Select(a => new PlaceWithStorageItemOnUnit()
                {
                    UnitId = a.UnitId,
                    Name = a.Name,
                    Count = a.Count,
                })
                .ToListAsync(cancellationToken);
                    
            return new GetAllPlacesOfStorageItemOnUnitSuccessResultContract() {Contains = result};
        }
    }   
}