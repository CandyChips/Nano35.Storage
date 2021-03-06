﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllSells
{
    public class GetAllSellsUseCase :
        UseCaseEndPointNodeBase<IGetAllSellsRequestContract, IGetAllSellsResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllSellsUseCase(IBus bus) { _bus = bus; }
        public override async Task<UseCaseResponse<IGetAllSellsResultContract>> Ask(IGetAllSellsRequestContract input) => 
            await new MasstransitUseCaseRequest<IGetAllSellsRequestContract, IGetAllSellsResultContract>(_bus, input)
                .GetResponse();
    }
}