using InternetTechnologies.DomainModels.Services.Interfaces;
using System;

namespace InternetTechnologies.DomainModels.Models.Entities
{
    [Serializable]
    public class MedicalCard : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }

        public int Growth { get; set; }

        public double Weigth { get; set; }

        public Blood Blood { get; set; }
    }
}
