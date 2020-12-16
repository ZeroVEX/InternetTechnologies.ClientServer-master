using InternetTechnologies.Client.BLL.Services.Interfaces;

namespace InternetTechnologies.Client.BLL.Models.DTO
{
    public class MedicalCardDto : IModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }

        public int Growth { get; set; }

        public double Weigth { get; set; }

        public Blood Blood { get; set; }
    }
}
