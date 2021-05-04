using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllCancellationDetails
{
    public class GetAllCancellationDetailsUseCase :
        UseCaseEndPointNodeBase<IGetAllCancellationDetailsRequestContract, IGetAllCancellationDetailsResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllCancellationDetailsUseCase(IBus bus){ _bus = bus; }
        public override async Task<UseCaseResponse<IGetAllCancellationDetailsResultContract>> Ask(IGetAllCancellationDetailsRequestContract input) => 
            await new MasstransitUseCaseRequest<IGetAllCancellationDetailsRequestContract, IGetAllCancellationDetailsResultContract>(_bus, input)
                .GetResponse();
    }
}
