using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllSelleDetails
{
    public class GetAllSelleDetailsUseCase :
        UseCaseEndPointNodeBase<IGetAllSelleDetailsRequestContract, IGetAllSelleDetailsResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllSelleDetailsUseCase(IBus bus) { _bus = bus; }
        public override async Task<UseCaseResponse<IGetAllSelleDetailsResultContract>> Ask(IGetAllSelleDetailsRequestContract input) => 
            await new MasstransitUseCaseRequest<IGetAllSelleDetailsRequestContract, IGetAllSelleDetailsResultContract>(_bus, input)
                .GetResponse();
    }
}