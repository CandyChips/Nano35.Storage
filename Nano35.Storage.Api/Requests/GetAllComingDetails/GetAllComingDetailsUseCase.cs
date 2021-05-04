
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
namespace Nano35.Storage.Api.Requests.GetAllComingDetails
{
    public class GetAllComingDetailsUseCase :
        UseCaseEndPointNodeBase<IGetAllComingDetailsRequestContract, IGetAllComingDetailsResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllComingDetailsUseCase(IBus bus) { _bus = bus; }
        public override async Task<UseCaseResponse<IGetAllComingDetailsResultContract>> Ask(IGetAllComingDetailsRequestContract input) => 
            await new MasstransitUseCaseRequest<IGetAllComingDetailsRequestContract, IGetAllComingDetailsResultContract>(_bus, input)
                .GetResponse();
    }
}