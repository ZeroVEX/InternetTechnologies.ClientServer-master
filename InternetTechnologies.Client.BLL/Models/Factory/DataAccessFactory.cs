using FactoryFromDal = InternetTechnologies.Client.DAL.Models.Factory.DataAccessFactory;
using DataAccessFromDal = InternetTechnologies.Client.DAL.Models.DataAccess;
using InternetTechnologies.Client.BLL.Services;
using InternetTechnologies.DomainModels.Models.Entities;
using InternetTechnologies.Client.BLL.Models.DTO;
using AutoMapper;
using InternetTechnologies.Client.BLL.Services.ValidationProviders;

namespace InternetTechnologies.Client.BLL.Models.Factory
{
    public static class DataAccessFactory
    {
        public static DataAccess GetDataAccess(string host = default, int port = default)
        {
            DataAccessFromDal dalDataAccess = null;

            IMapper mapper = new MapperConfiguration(config 
                                                        => config.AddProfile(new MapperProfile()))
                                                                                                .CreateMapper();

            dalDataAccess = FactoryFromDal.GetDataAccess(host, port);

            return new DataAccess(
                 new GenericService<MedicalCardDto, MedicalCard>(
                                                                    dalDataAccess.ChemicalElement, 
                                                                    new MedicalCardValidationProvider(),
                                                                    mapper));
        }
    }
}
