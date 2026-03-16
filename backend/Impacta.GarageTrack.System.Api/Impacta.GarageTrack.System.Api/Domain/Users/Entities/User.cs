using Impacta.GarageTrack.System.Api.Domain.Kernel;

namespace Impacta.GarageTrack.System.Api.Domain.Users.Entities
{
    public class User : EntityBase<long>
    {
        private User()
        {
            Name = string.Empty;
            Email = string.Empty;
            PasswordHash = string.Empty;
            CompanyId = 0;
            Role = UserRole.User;
        }

        public User(string name, string email, string passwordHash, long companyId, UserRole userRole)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            CompanyId = companyId;
            Role = userRole;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
        public long CompanyId { get; set; }
    }
}
