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

namespace Nano35.Storage.Processor.UseCases.GetAllSells
{
    public class GetAllSellsRequest :
        IPipelineNode<
            IGetAllSellsRequestContract,
            IGetAllSellsResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetAllSellsRequest(
            ApplicationContext context, 
            IBus bus)
        {
            _context = context;
            _bus = bus;
        }
        
        private class GetAllSellsSuccessResultContract : 
            IGetAllSellsSuccessResultContract
        {
            public List<SelleViewModel> Data { get; set; }
        }

        public async Task<IGetAllSellsResultContract> Ask
            (IGetAllSellsRequestContract input, CancellationToken cancellationToken)
        {
            var queue = await _context
                .Sells
                .Where(c => c.InstanceId == input.InstanceId)
                .ToListAsync(cancellationToken);


            var result = queue
                .Select(a =>
                {
                    var res = new SelleViewModel()
                    {
                        Id = a.Id,
                        Number = a.Number,
                        Date = a.Date,
                        Cash = a.Details
                            .Select(f => f.Price * f.Count)
                            .Sum()
                    };
                    var getClientStringById = new GetClientStringById(_bus,
                        new GetClientStringByIdRequestContract() {ClientId = a.ClientId});
                    res.Client = getClientStringById.GetResponse().Result switch
                    {
                        IGetClientStringByIdSuccessResultContract success => success.Data,
                        IGetClientStringByIdErrorResultContract => "",
                        _ => ""
                    };
                    var getUnitStringById = new GetUnitStringById(_bus,
                        new GetUnitStringByIdRequestContract() {UnitId = a.Details.FirstOrDefault().FromUnitId});
                    res.Unit = getUnitStringById.GetResponse().Result switch
                    {
                        IGetUnitStringByIdSuccessResultContract success => success.Data,
                        IGetUnitStringByIdErrorResultContract => "",
                        _ => ""
                    };

                    return res;
                }).ToList();
            
            return new GetAllSellsSuccessResultContract() {Data = result};
        }
    }   
}