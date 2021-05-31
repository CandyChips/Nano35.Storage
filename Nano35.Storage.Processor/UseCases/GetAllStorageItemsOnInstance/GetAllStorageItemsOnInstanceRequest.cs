using System;
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

namespace Nano35.Storage.Processor.UseCases.GetAllStorageItemsOnInstance
{
    public class GetAllStorageItemsOnInstanceRequest : UseCaseEndPointNodeBase<IGetAllStorageItemsOnInstanceContract, IGetAllStorageItemsOnInstanceResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetAllStorageItemsOnInstanceRequest(ApplicationContext context, IBus bus)
        {
            _context = context;
            _bus = bus;
        }
        
        public override async Task<UseCaseResponse<IGetAllStorageItemsOnInstanceResultContract>> Ask(
            IGetAllStorageItemsOnInstanceContract input, 
            CancellationToken cancellationToken)
        {
            var storageItem = (await _context
                .Warehouses
                .Where(c => c.InstanceId == input.InstanceId && c.IsDeleted == false)
                .ToListAsync(cancellationToken))
                .GroupBy(g => g.StorageItem, e => e)
                .Select(a =>
                {
                    var r = new StorageItemOnInstanceViewModel
                    {
                        Count = a.Sum(s => s.Count),
                        Item =
                            new StorageItemWarehouseView()
                            {
                                Id = a.Key.Id,
                                Name = a.Key.ToString(),
                                PurchasePrice = (double) (a.Key.PurchasePrice),
                                RetailPrice = (double) (a.Key.RetailPrice),
                                Images = new MasstransitUseCaseRequest<IGetImagesOfStorageItemRequestContract, IGetImagesOfStorageItemResultContract>(_bus, new GetImagesOfStorageItemRequestContract() { StorageItemId = a.Key.Id }).GetResponse().Result.Success.Images
                            },
                    };
                    var getUnitStringByIdRequestContract = 
                        new MasstransitUseCaseRequest<IGetUnitStringByIdRequestContract, IGetUnitStringByIdResultContract>(_bus, new GetUnitStringByIdRequestContract() {UnitId = a.First().UnitId}).GetResponse().Result;
                    r.Unit = getUnitStringByIdRequestContract.IsSuccess() ? getUnitStringByIdRequestContract.Success.Data : throw new Exception();

                    return r;
                })
                .Where(e => e.Count > 0)
                .ToList();

            return new UseCaseResponse<IGetAllStorageItemsOnInstanceResultContract>(
                new GetAllStorageItemsOnInstanceResultContract() {Contains = storageItem});
        }
    }   
}