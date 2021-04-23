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

namespace Nano35.Storage.Processor.UseCases.GetSelleById
{
    public class GetSelleByIdRequest :
        EndPointNodeBase<IGetSelleByIdRequestContract, IGetSelleByIdResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetSelleByIdRequest(
            ApplicationContext context, IBus bus)
        {
            _context = context;
            _bus = bus;
        }
        
        public override async Task<IGetSelleByIdResultContract> Ask(
            IGetSelleByIdRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = await _context.Sells
                .FirstAsync(c => c.Id == input.Id, cancellationToken: cancellationToken);

            var selle = new SelleViewModel()
            {
                Id = result.Id,
                Cash = result.Details.Sum(s => s.Price * s.Count),
                Date = result.Date,
                Number = result.Number
            };
            
            var getUnitStringRequest = new GetUnitStringById(_bus,
                new GetUnitStringByIdRequestContract() {UnitId = result.Details.First().FromUnitId});
            selle.Unit = getUnitStringRequest.GetResponse().Result switch
            {
                IGetUnitStringByIdSuccessResultContract success => success.Data,
                _ => throw new Exception()
            };
                    

            return new GetSelleByIdSuccessResultContract() {Selle = selle};
        }
    }   
}