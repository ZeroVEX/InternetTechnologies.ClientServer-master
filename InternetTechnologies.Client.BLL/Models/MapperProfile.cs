using AutoMapper;
using InternetTechnologies.Client.BLL.Models.DTO;
using InternetTechnologies.DomainModels.Models.Entities;

namespace InternetTechnologies.Client.BLL.Models
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<MedicalCard, MedicalCardDto>().ReverseMap();
        }
    }
}
