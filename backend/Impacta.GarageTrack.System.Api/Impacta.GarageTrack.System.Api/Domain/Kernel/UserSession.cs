namespace Impacta.GarageTrack.System.Api.Domain.Kernel
{
    public class UserSession
    {
        public UserSession(long userId,
            long companyId,
            string companyName,
            string userName,
            string email,
            UserRole userRole)
        {
            UserId = userId;
            CompanyId = companyId;
            CompanyName = companyName;
            UserName = userName;
            Email = email;
            UserRole = userRole;
        }

        public long UserId { get; }
        public long CompanyId { get; }
        public string CompanyName { get; }
        public string UserName { get; }
        public string Email { get; set; }
        public UserRole UserRole { get; set; }
    }
}
