using InternetTechnologies.Client.BLL.Models.DTO;
using InternetTechnologies.Client.BLL.Services.Interfaces;

namespace InternetTechnologies.Client.BLL.Models
{
    public class DataAccess
    {
        public IService<MedicalCardDto> MedicalCard { get; }

        public DataAccess(IService<MedicalCardDto> medicalCard)
        {
            MedicalCard = medicalCard;
        }
    }
}
