using System.ComponentModel;

namespace MannDBCollector.Common
{
    public class ModelPageModel
    {
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

        public string DetailPageUrl { get; set; }
    }
}
