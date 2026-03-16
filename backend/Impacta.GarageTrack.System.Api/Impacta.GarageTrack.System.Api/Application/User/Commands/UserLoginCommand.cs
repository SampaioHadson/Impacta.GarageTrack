using Impacta.GarageTrack.System.Api.Application.Kernel;
using Impacta.GarageTrack.System.Api.Application.User.Services;

namespace Impacta.GarageTrack.System.Api.Application.User.Commands
{
    public class UserLoginCommand : IUserLoginCommand
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly IAuthenticatorService _authenticatorService;

        public UserLoginCommand(IUnityOfWork unityOfWork,
            IAuthenticatorService authenticatorService)
        {
            _unityOfWork = unityOfWork;
            _authenticatorService = authenticatorService;
        }

        public async Task<Result<Response>> HandleAsync(Request command)
        {
            var users = await _unityOfWork.UserRepository.GetByEmailAsync(command.Email);
            if (users == null || !users.Any())
            {
                return Result<Response>.Failure("Invalid email or password.");
            }

            var userWithSamePass = users.Where(u => u.PasswordHash == command.Password);
            if (!userWithSamePass.Any())
            {
                return Result<Response>.Failure("Invalid email or password.");
            }

            if (users.Count() > 1 && !command.CompanyId.HasValue)
            {
                var companies = new List<Response.CompanyInfo>();
                foreach (var user in users)
                {
                    var company = await _unityOfWork.CompanyRepository.GetByIdAsync(user.CompanyId);
                    if (company != null)
                    {
                        companies.Add(new Response.CompanyInfo(company.Id, company.Name));
                    }
                }

                return Result<Response>.Success(new Response
                {
                    Companies = companies
                });
            }

            var userFirst = !command.CompanyId.HasValue 
                ? userWithSamePass.First()
                : userWithSamePass.First(x => x.CompanyId == command.CompanyId.Value);

            var companyFirst = await _unityOfWork.CompanyRepository.GetByIdAsync(userFirst.CompanyId);
     
            if (companyFirst == null) 
                return Result<Response>.Failure("Company not found for the user.");

            var session = new Domain.Kernel.UserSession(userFirst.Id, companyFirst.Id, companyFirst.Name, userFirst.Name, userFirst.Email, userFirst.Role);
            var token = _authenticatorService.GenerateToken(session);
            return Result<Response>.Success(new Response
            {
                UserData = new Response.UserInfo(userFirst.Id, userFirst.Name, userFirst.Email, userFirst.CompanyId, companyFirst.Name),
                TokenData = new Response.TokenInfo(token, DateTime.UtcNow.AddHours(1))
            });
        }

        public record struct Request(string Email, string Password, long? CompanyId);

        public class Response
        {
            public TokenInfo? TokenData { get; set; }
            public UserInfo? UserData { get; set; }
            public List<CompanyInfo>? Companies { get; set; }

            public class CompanyInfo
            {
                public CompanyInfo(long id, string name)
                {
                    Id = id;
                    Name = name;
                }

                public long Id { get; set; }
                public string Name { get; set; }
            }

            public class TokenInfo
            {
                public TokenInfo(string accessToken, DateTime expiration)
                {
                    AccessToken = accessToken;
                    Expiration = expiration;
                }

                public string AccessToken { get; set; }
                public DateTime Expiration { get; set; }
            }

            public class UserInfo
            {
                public UserInfo(long id, string name, string email, long companyId, string companyName)
                {
                    Id = id;
                    Name = name;
                    Email = email;
                    CompanyId = companyId;
                    CompanyName = companyName;
                }

                public long Id { get; set; }
                public string Name { get; set; }
                public string Email { get; set; }
                public long CompanyId { get; set; }
                public string CompanyName { get; set; }
            }
        }
    }

    public interface IUserLoginCommand : ICommand<UserLoginCommand.Request, Result<UserLoginCommand.Response>>
    {
    }
}
