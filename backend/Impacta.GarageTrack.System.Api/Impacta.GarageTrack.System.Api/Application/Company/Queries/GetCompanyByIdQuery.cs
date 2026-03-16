using Impacta.GarageTrack.System.Api.Application.Kernel;

namespace Impacta.GarageTrack.System.Api.Application.Company.Queries
{
    public class GetCompanyByIdQuery : IGetCompanyByIdQuery
    {
        private readonly IUnityOfWork _unityOfWork;

        public GetCompanyByIdQuery(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        public async Task<Result<Domain.Company.Entities.Company>> Handle(long request)
        {
            var company = await _unityOfWork.CompanyRepository.GetByIdAsync(request);
            if (company == null)
            {
                return Result<Domain.Company.Entities.Company>.Failure("Company not found");
            }

            return Result<Domain.Company.Entities.Company>.Success(company);
        }
    }

    public interface IGetCompanyByIdQuery : IQuery<long, Result<Domain.Company.Entities.Company>>
    {
    }
}
