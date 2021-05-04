using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllCancellations
{
    public class GetAllCancellationsUseCase :
        UseCaseEndPointNodeBase<IGetAllCancellationsRequestContract, IGetAllCancellationsResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllCancellationsUseCase(IBus bus) { _bus = bus; }
        public override async Task<UseCaseResponse<IGetAllCancellationsResultContract>> Ask(IGetAllCancellationsRequestContract input) => 
            await new MasstransitUseCaseRequest<IGetAllCancellationsRequestContract, IGetAllCancellationsResultContract>(_bus, input)
                .GetResponse();
    }
}