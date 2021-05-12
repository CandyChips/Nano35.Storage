using System;
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

namespace Nano35.Storage.Processor.UseCases.GetAllComings
{
    public class GetAllComingsRequest : UseCaseEndPointNodeBase<IGetAllComingsRequestContract, IGetAllComingsResultContract>
    {
        private record ComingDto(Guid Id, string Number, DateTime Date, double Cash, Guid ClientId, Guid InstanceId);
        private readonly ApplicationContext _context;
        private readonly IBus _bus;
        public GetAllComingsRequest(ApplicationContext context, IBus bus) { _context = context; _bus = bus; }
        public override async Task<UseCaseResponse<IGetAllComingsResultContract>> Ask(
            IGetAllComingsRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = (await _context
                .Comings
                .Where(c => c.InstanceId == input.InstanceId)
                .ToListAsync(cancellationToken))
                .Select(a => new ComingDto(a.Id, a.Number, a.Date, a.Details.Select(f => f.Price * f.Count).Sum(), a.ClientId, a.InstanceId))
                .ToList();
            var response = new List<ComingViewModel>();
            foreach (var item in result)
            {
                var res = 
                    new ComingViewModel()
                        {Id = item.Id,
                         Number = item.Number,
                         Cash = item.Cash,
                         Date = item.Date};
                
                var getClientStringByIdRequestContract = new MasstransitUseCaseRequest<IGetClientStringByIdRequestContract, IGetClientStringByIdResultContract>(_bus, new GetClientStringByIdRequestContract() {ClientId = item.ClientId, InstanceId = item.InstanceId}).GetResponse().Result;
                if (getClientStringByIdRequestContract.IsSuccess()) res.Client = getClientStringByIdRequestContract.Success.Data;
                else return Pass(getClientStringByIdRequestContract.Error);
                
                var getUnitStringByIdRequestContract = new MasstransitUseCaseRequest<IGetUnitStringByIdRequestContract, IGetUnitStringByIdResultContract>(_bus, new GetUnitStringByIdRequestContract() {UnitId = input.UnitId}).GetResponse().Result;
                if (getUnitStringByIdRequestContract.IsSuccess()) res.Unit = getUnitStringByIdRequestContract.Success.Data;
                else return Pass(getUnitStringByIdRequestContract.Error);
                
                response.Add(res);
            }

            return Pass(new GetAllComingsResultContract(){Data = response});
        }
    }   
}