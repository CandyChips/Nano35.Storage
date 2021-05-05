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
    public class GetAllComingsRequest :
        UseCaseEndPointNodeBase<IGetAllComingsRequestContract, IGetAllComingsResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetAllComingsRequest(
            ApplicationContext context, 
            IBus bus)
        {
            _context = context;
            _bus = bus;
        }
        
        public override async Task<UseCaseResponse<IGetAllComingsResultContract>> Ask
            (IGetAllComingsRequestContract input, CancellationToken cancellationToken)
        {
            var result = (await _context
                .Comings
                .Where(c => c.InstanceId == input.InstanceId)
                .ToListAsync(cancellationToken))
                .Select(a =>
                {
                    var r = new ComingViewModel
                    {
                        Id = a.Id,
                        Number = a.Number,
                        Date = a.Date,
                        Cash = a.Details
                            .Select(f => f.Price * f.Count)
                            .Sum()
                    };
                    var getClientStringByIdRequestContract = 
                        new MasstransitUseCaseRequest<IGetClientStringByIdRequestContract, IGetClientStringByIdResultContract>(_bus, new GetClientStringByIdRequestContract() {ClientId = a.ClientId}).GetResponse().Result;
                    r.Client = getClientStringByIdRequestContract.IsSuccess() ? getClientStringByIdRequestContract.Success.Data : throw new Exception();
                    var getUnitStringByIdRequestContract = 
                        new MasstransitUseCaseRequest<IGetUnitStringByIdRequestContract, IGetUnitStringByIdResultContract>(_bus, new GetUnitStringByIdRequestContract() {UnitId = input.UnitId}).GetResponse().Result;
                    r.Unit = getUnitStringByIdRequestContract.IsSuccess() ? getUnitStringByIdRequestContract.Success.Data : throw new Exception();

                    return r;
                })
                .ToList();

            return new UseCaseResponse<IGetAllComingsResultContract>(new GetAllComingsResultContract(){Data = result});
        }
    }   
}