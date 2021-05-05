using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllPlacesOfStorageItemOnInstance
{
    public class GetAllPlacesOfStorageItemOnInstanceRequest :
        UseCaseEndPointNodeBase<IGetAllPlacesOfStorageItemOnInstanceContract, IGetAllPlacesOfStorageItemOnInstanceResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetAllPlacesOfStorageItemOnInstanceRequest(
            ApplicationContext context,
            IBus bus)
        {
            _context = context;
            _bus = bus;
        }
        
        public override async Task<UseCaseResponse<IGetAllPlacesOfStorageItemOnInstanceResultContract>> Ask
            (IGetAllPlacesOfStorageItemOnInstanceContract input, 
            CancellationToken cancellationToken)
        {
            var result = (await _context
                .Warehouses
                .Where(c => c.StorageItemId == input.StorageItemId)
                .ToListAsync(cancellationToken))
                .Select(a =>
                {
                    var r = new PlaceWithStorageItemOnInstance()
                    {
                        UnitId = a.UnitId,
                        Name = a.Name,
                        Count = a.Count
                    };
                    var getUnitStringByIdRequestContract = 
                        new MasstransitUseCaseRequest<IGetUnitStringByIdRequestContract, IGetUnitStringByIdResultContract>(_bus, new GetUnitStringByIdRequestContract() {UnitId = a.UnitId}).GetResponse().Result;
                    r.Unit = getUnitStringByIdRequestContract.IsSuccess() ? getUnitStringByIdRequestContract.Success.Data : throw new Exception();

                    return r;
                })
                .ToList();
            return new UseCaseResponse<IGetAllPlacesOfStorageItemOnInstanceResultContract>(
                new GetAllPlacesOfStorageItemOnInstanceResultContract() {Contains = result});
        }
    }   
}