using InternetTechnologies.Client.DAL.Services.Repositories;
using InternetTechnologies.DomainModels.Models.Entities;
using System;

namespace InternetTechnologies.Client.DAL.Models.Factory
{
    public static class DataAccessFactory
    {
        public static DataAccess GetDataAccess(string host = default, int port = default, string apiAddress = default)
        {
            DataAccess dataAccess = null;

            if (!string.IsNullOrEmpty(host) && port > 0)
            {
                dataAccess = new DataAccess(new NetworkRepository<MedicalCard>(host, port));
            }
            else if (!string.IsNullOrEmpty(apiAddress))
            {
                dataAccess = new DataAccess(new HttpRepository<MedicalCard>(apiAddress));
            }
            else
            {
                throw new ArgumentException("Invalid values");
            }
           
            return dataAccess;
        }
    }
}
