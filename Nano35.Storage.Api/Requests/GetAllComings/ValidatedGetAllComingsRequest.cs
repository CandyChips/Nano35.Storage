using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllComings
{
    public class GetAllComingsValidatorErrorResult : 
        IGetAllComingsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllComingsRequest:
        PipeNodeBase<IGetAllComingsRequestContract, IGetAllComingsResultContract>
    {
        public ValidatedGetAllComingsRequest(
            IPipeNode<IGetAllComingsRequestContract, IGetAllComingsResultContract> next) :
            base(next) { }

        public override async Task<IGetAllComingsResultContract> Ask(
            IGetAllComingsRequestContract input)
        {
            return await DoNext(input);
        }
    }
}