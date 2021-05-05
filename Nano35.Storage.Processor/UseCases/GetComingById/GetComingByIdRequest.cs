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

namespace Nano35.Storage.Processor.UseCases.GetComingById
{
    public class GetComingByIdRequest :
        UseCaseEndPointNodeBase<IGetComingByIdRequestContract, IGetComingByIdResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetComingByIdRequest(
            ApplicationContext context, IBus bus)
        {
            _context = context;
            _bus = bus;
        }
        
        public override async Task<UseCaseResponse<IGetComingByIdResultContract>> Ask(
            IGetComingByIdRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = await _context.Comings
                .FirstAsync(c => c.Id == input.Id, cancellationToken: cancellationToken);

            if (result == null)
                return new UseCaseResponse<IGetComingByIdResultContract>("не найдено");

            var coming = new ComingViewModel()
            {
                Id = result.Id,
                Cash = result.Details.Sum(s => s.Price * s.Count),
                Date = result.Date,
                Number = result.Number
            };
            
            var getUnitStringByIdRequestContract = 
                new MasstransitUseCaseRequest<IGetUnitStringByIdRequestContract, IGetUnitStringByIdResultContract>(_bus, new GetUnitStringByIdRequestContract() {UnitId = result.Details.First().ToUnitId}).GetResponse().Result;
            coming.Unit = getUnitStringByIdRequestContract.IsSuccess() ? getUnitStringByIdRequestContract.Success.Data : throw new Exception();

            var getClientStringByIdRequestContract = 
                new MasstransitUseCaseRequest<IGetClientStringByIdRequestContract, IGetClientStringByIdResultContract>(_bus, new GetClientStringByIdRequestContract() {ClientId = result.ClientId}).GetResponse().Result;
            coming.Client = getClientStringByIdRequestContract.IsSuccess() ? getClientStringByIdRequestContract.Success.Data : throw new Exception();

            return new UseCaseResponse<IGetComingByIdResultContract>(
                new GetComingByIdResultContract() {Coming = coming});
        }
    }   
}