using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.files;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllStorageItemsOnUnit
{
    public class GetAllStorageItemsOnUnitRequest :
        UseCaseEndPointNodeBase<IGetAllStorageItemsOnUnitContract, IGetAllStorageItemsOnUnitResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetAllStorageItemsOnUnitRequest(
            ApplicationContext context, IBus bus)
        {
            _context = context;
            _bus = bus;
        }
        
        public override async Task<UseCaseResponse<IGetAllStorageItemsOnUnitResultContract>> Ask(
            IGetAllStorageItemsOnUnitContract input, 
            CancellationToken cancellationToken)
        {
            var storageItems = (await _context
                .Warehouses
                .Where(c => c.UnitId == input.UnitId)
                .ToListAsync(cancellationToken))
                .GroupBy(e => e.StorageItem)
                .Select(a =>
                {
                    var r = new StorageItemOnUnitViewModel
                    {
                        Count = a.Sum(s => s.Count),
                        Item =
                            new StorageItemWarehouseView()
                            {
                                Id = a.Key.Id,
                                Name = a.Key.ToString(),
                                PurchasePrice = (double) a.Key.PurchasePrice,
                                RetailPrice = (double) a.Key.RetailPrice,
                                //Images = (new GetImagesOfStorageItem(_bus,new GetImagesOfStorageItemRequestContract() { StorageItemId = a.StorageItem.Id })
                                //    .GetResponse()
                                //    .Result as IGetImagesOfStorageItemSuccessResultContract)?
                                //    .Images
                            }
                    };
                    return r;
                })
                .ToList();

            return Pass(new GetAllStorageItemsOnUnitResultContract() {Contains = storageItems});
        }
    }   
}