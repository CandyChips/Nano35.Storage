using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllMoves
{
    public class GetAllMovesUseCase :
        UseCaseEndPointNodeBase<IGetAllMovesRequestContract, IGetAllMovesResultContract>
    {
        private readonly IBus _bus;
        public GetAllMovesUseCase(IBus bus) { _bus = bus; }
        public override async Task<UseCaseResponse<IGetAllMovesResultContract>> Ask(IGetAllMovesRequestContract input) => 
            await new MasstransitUseCaseRequest<IGetAllMovesRequestContract, IGetAllMovesResultContract>(_bus, input)
                .GetResponse();
    }
}