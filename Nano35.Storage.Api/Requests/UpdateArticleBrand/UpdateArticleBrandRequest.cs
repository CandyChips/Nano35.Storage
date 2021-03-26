using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateArticleBrand
{
    public class UpdateArticleBrandRequest :
        MasstransitRequest
        <IUpdateArticleBrandRequestContract,
            IUpdateArticleBrandResultContract,
            IUpdateArticleBrandSuccessResultContract,
            IUpdateArticleBrandErrorResultContract>
    {
        public UpdateArticleBrandRequest(IBus bus) : base(bus) {}
    }
}