namespace Impacta.GarageTrack.System.Api.Application.User.Services
{
    public interface IAuthenticatorService
    {
        public string GenerateToken(Domain.Kernel.UserSession userSession);
    }
}
