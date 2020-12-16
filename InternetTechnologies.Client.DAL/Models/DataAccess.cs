using InternetTechnologies.DomainModels.Models.Entities;
using InternetTechnologies.DomainModels.Services.Interfaces;

namespace InternetTechnologies.Client.DAL.Models
{
    public class DataAccess
    {
        public IRepository<MedicalCard> ChemicalElement { get; }

        public DataAccess(IRepository<MedicalCard> chemicalElement)
        {
            ChemicalElement = chemicalElement;
        }
    }
}
