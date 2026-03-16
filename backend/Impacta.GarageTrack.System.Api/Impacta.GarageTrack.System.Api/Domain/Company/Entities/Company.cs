using Impacta.GarageTrack.System.Api.Domain.Kernel;

namespace Impacta.GarageTrack.System.Api.Domain.Company.Entities
{
    public class Company : EntityBase<long>
    {
        private Company()
        {
            Name = string.Empty;
            CNPJ = string.Empty;
        }

        public Company(string name, string cnpj)
        {
            Name = name;
            CNPJ = cnpj;
        }

        public string Name { get; set; }
        public string CNPJ { get; set; }
    }
}
