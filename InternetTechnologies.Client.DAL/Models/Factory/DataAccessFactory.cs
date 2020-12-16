using InternetTechnologies.Client.DAL.Services.Repositories;
using InternetTechnologies.DomainModels.Models.Entities;
using System;

namespace InternetTechnologies.Client.DAL.Models.Factory
{
    public static class DataAccessFactory
    {
        public static DataAccess GetDataAccess(string host = default, int port = default)
        {
            DataAccess dataAccess = null;

            if (string.IsNullOrEmpty(host) || port < 0)
            {
                throw new ArgumentException("Invalid host or port");
            }

            dataAccess = new DataAccess(new NetworkRepository<MedicalCard>(host, port));

            return dataAccess;
        }
    }
}
