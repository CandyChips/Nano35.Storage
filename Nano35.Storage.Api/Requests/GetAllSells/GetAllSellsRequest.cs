using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllSells
{
    public class GetAllSellsRequest:
        MasstransitRequest<
            IGetAllSellsRequestContract,
            IGetAllSellsResultContract,
            IGetAllSellsSuccessResultContract,
            IGetAllSellsErrorResultContract>
    {
        public GetAllSellsRequest(IBus bus) : base(bus) {}
    }
}