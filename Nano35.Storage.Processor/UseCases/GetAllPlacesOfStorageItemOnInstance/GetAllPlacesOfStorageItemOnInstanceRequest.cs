using System.Collections.Generic;
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
        IPipelineNode<
            IGetAllPlacesOfStorageItemOnInstanceContract,
            IGetAllPlacesOfStorageItemOnInstanceResultContract>
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
        
        private class GetAllPlacesOfStorageItemOnInstanceSuccessResultContract : 
            IGetAllPlacesOfStorageItemOnInstanceSuccessResultContract
        {
            public List<PlaceWithStorageItemOnInstance> Contains { get; set; }
        }
        
        public async Task<IGetAllPlacesOfStorageItemOnInstanceResultContract> Ask
            (IGetAllPlacesOfStorageItemOnInstanceContract input, 
            CancellationToken cancellationToken)
        {
            var queue = await _context
                .Warehouses
                .Where(c =>
                    c.StorageItemId == input.StorageItemId).ToListAsync(cancellationToken);


            var result = queue
                .Select(a =>
                {
                    var res = new PlaceWithStorageItemOnInstance()
                    {
                        Id = a.UnitId,
                        Name = a.Name,
                        Count = a.Count
                    };
                    var getUnitStringById = new GetUnitStringById(_bus,
                        new GetUnitStringByIdRequestContract() {UnitId = a.UnitId});
                    res.Unit = getUnitStringById.GetResponse().Result switch
                    {
                        IGetUnitStringByIdSuccessResultContract success => success.Data,
                        IGetUnitStringByIdErrorResultContract => "",
                        _ => ""
                    };

                    return res;
                }).ToList();
            
            return new GetAllPlacesOfStorageItemOnInstanceSuccessResultContract() {Contains = result};
        }
    }   
}