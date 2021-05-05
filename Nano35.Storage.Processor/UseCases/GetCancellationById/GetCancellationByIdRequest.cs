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

namespace Nano35.Storage.Processor.UseCases.GetCancellationById
{
    public class GetCancellationByIdRequest :
        UseCaseEndPointNodeBase<IGetCancellationByIdRequestContract, IGetCancellationByIdResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetCancellationByIdRequest(
            ApplicationContext context, IBus bus)
        {
            _context = context;
            _bus = bus;
        }
        
        public override async Task<UseCaseResponse<IGetCancellationByIdResultContract>> Ask(
            IGetCancellationByIdRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = await _context.Cancellations
                .FirstAsync(c => c.Id == input.Id, cancellationToken: cancellationToken);
            
            if (result == null)
                return new UseCaseResponse<IGetCancellationByIdResultContract>("Не найдено");
            
            var cancellation = new CancellationViewModel()
            {
                Id = result.Id,
                Comment = result.Comment,
                Date = result.Date,
                Number = result.Number
            };
            
            var getUnitStringByIdRequestContract = 
                new MasstransitUseCaseRequest<IGetUnitStringByIdRequestContract, IGetUnitStringByIdResultContract>(_bus, new GetUnitStringByIdRequestContract() {UnitId = result.Details.First().FromUnitId}).GetResponse().Result;
            cancellation.Unit = getUnitStringByIdRequestContract.IsSuccess() ? getUnitStringByIdRequestContract.Success.Data : throw new Exception();

            return new UseCaseResponse<IGetCancellationByIdResultContract>(new GetCancellationByIdResultContract(){Cancellation = cancellation});
        }
    }   
}