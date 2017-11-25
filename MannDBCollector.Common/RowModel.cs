using System.ComponentModel;

namespace MannDBCollector.Common
{
    public class RowModel
    {
        [DisplayName("Kategori")]
        public string VehicleType { get; set; }
        [DisplayName("Üretici")]
        public string Producer { get; set; }
        [DisplayName("Model")]
        public string Model { get; set; }
        [DisplayName("Model Tipi")]
        public string ModelType { get; set; }
        [DisplayName("Motor Kodu")]
        public string MotorCode { get; set; }
        [DisplayName("kW")]
        public string kW { get; set; }
        [DisplayName("PS")]
        public string PS { get; set; }
        [DisplayName("Year of Manufacture")]
        public string Year { get; set; }

        // filters
        [DisplayName("Hava Filtresi")]
        public string AirFilters { get; set; }
        [DisplayName("Ölçü Hava")]
        public string AirFilterDimensions { get; set; }

        [DisplayName("Yağ Filtresi")]
        public string OilFilters { get; set; }
        [DisplayName("Ölçü Yağ")]
        public string OilFilterDimensions { get; set; }

        [DisplayName("Yakıt Filtresi")]
        public string FuelFilters { get; set; }
        [DisplayName("Ölçü Yakıt")]
        public string FuelFilterDimensions { get; set; }

        [DisplayName("İç Mekan Filtresi")]
        public string InteriorFilters { get; set; }
        [DisplayName("Ölçü İç Mekan")]
        public string InteriorFilterDimensions { get; set; }

        [DisplayName("Diğer Filtreler")]
        public string OtherFilters { get; set; }
        [DisplayName("Ölçü Diğer Filtreler")]
        public string OtherFilterDimensions { get; set; }
    }
}
