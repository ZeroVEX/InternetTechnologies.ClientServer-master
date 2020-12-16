using System.ComponentModel.DataAnnotations;

namespace InternetTechnologies.Server.Api.Models
{
    public class MedicalCardDto
    {
        [Required]
        [Range(AppValues.IdMinValue, AppValues.IdMaxValue)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(AppValues.YearMinValue, AppValues.YearMaxValue)]
        public int Year { get; set; }

        [Required]
        [Range(AppValues.GrowthMinValue, AppValues.GrowthMaxValue)]
        public int Growth { get; set; }

        [Required]
        [Range(AppValues.WeightMinValue, AppValues.WeightMaxValue)]
        public double Weigth { get; set; }

        [Required]
        [Range(AppValues.BloodMinValue, AppValues.BloodMaxValue)]
        public int Blood { get; set; }
    }
}
