using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllComings
{
    public class GetAllComingsRequest :
        MasstransitRequest<
            IGetAllComingsRequestContract,
            IGetAllComingsResultContract,
            IGetAllComingsSuccessResultContract,
            IGetAllComingsErrorResultContract>
    {
        public GetAllComingsRequest(IBus bus) : base(bus) {}
    }
}