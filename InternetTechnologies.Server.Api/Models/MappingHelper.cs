using InternetTechnologies.DomainModels.Models.Entities;

namespace InternetTechnologies.Server.Api.Models
{
    public static class MappingHelper
    {
        public static MedicalCard ToModel(this MedicalCardDto medicalCardDto)
        {
            return new MedicalCard
            {
                Id = medicalCardDto.Id,
                Name = medicalCardDto.Name,
                Year = medicalCardDto.Year,
                Growth = medicalCardDto.Growth,
                Weigth = medicalCardDto.Weigth,
                Blood = (Blood)medicalCardDto.Blood,
            };
        }

        public static MedicalCardDto ToDto(this MedicalCard medicalCard)
        {
            return new MedicalCardDto
            {
                Id = medicalCard.Id,
                Name = medicalCard.Name,
                Year = medicalCard.Year,
                Growth = medicalCard.Growth,
                Weigth = medicalCard.Weigth,
                Blood = (int)medicalCard.Blood,
            };
        }
    }
}
