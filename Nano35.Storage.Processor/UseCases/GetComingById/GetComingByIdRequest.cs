using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Requests;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetComingById
{
    public class GetComingByIdRequest :
        EndPointNodeBase<IGetComingByIdRequestContract, IGetComingByIdResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetComingByIdRequest(
            ApplicationContext context, IBus bus)
        {
            _context = context;
            _bus = bus;
        }
        
        public override async Task<IGetComingByIdResultContract> Ask(
            IGetComingByIdRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = await _context.Comings
                .FirstAsync(c => c.Id == input.Id, cancellationToken: cancellationToken);

            var coming = new ComingViewModel()
            {
                Id = result.Id,
                Cash = result.Details.Sum(s => s.Price * s.Count),
                Date = result.Date,
                Number = result.Number
            };
            
            
            var getUnitStringRequest = new GetUnitStringById(_bus,
                new GetUnitStringByIdRequestContract() {UnitId = result.Details.First().ToUnitId});
            coming.Unit = getUnitStringRequest.GetResponse().Result switch
            {
                IGetUnitStringByIdSuccessResultContract success => success.Data,
                _ => throw new Exception()
            };
                    
            var getClientStringRequest = new GetClientStringById(_bus,
                new GetClientStringByIdRequestContract() { ClientId = result.ClientId });
            coming.Client = getClientStringRequest.GetResponse().Result switch
            {
                IGetClientStringByIdSuccessResultContract success => success.Data,
                _ => throw new Exception()
            };

            return new GetComingByIdSuccessResultContract() {Coming = coming};
        }
    }   
}