using DataCollector.Common;
using DataCollector.Common.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace DataCollector.IO.Tests
{
    [TestClass]
    public class PlanFileTests
    {
        [TestMethod]
        public void Save_NonEmptyArray_FileExists()
        {
            PlanFile planFile = new PlanFile();
            Dictionary<string, ITitleLinkPair[]> vehicleTypeProducers = new Dictionary<string, ITitleLinkPair[]>();
            vehicleTypeProducers.Add("Otomobil", new TitleLinkPair[]
            {
                new TitleLinkPair
                {
                    Title = "Acura",
                    Link = "http://catalog.mann-filter.com/EU/tur/vehicle/MANN-FILTER%20Katalog%20Europa/Ara%C3%A7lar/Otomobiller%20+%20Ticari%20Ara%C3%A7lar/ACURA"
                },
                new TitleLinkPair
                {
                    Title = "Alfa Romeo",
                    Link = "http://catalog.mann-filter.com/EU/tur/vehicle/MANN-FILTER%20Katalog%20Europa/Ara%C3%A7lar/Otomobiller%20+%20Ticari%20Ara%C3%A7lar/ALFA%20ROMEO"
                },
                new TitleLinkPair
                {
                    Title = "Audi",
                    Link = "http://catalog.mann-filter.com/EU/tur/vehicle/MANN-FILTER%20Katalog%20Europa/Ara%C3%A7lar/Otomobiller%20+%20Ticari%20Ara%C3%A7lar/AUDI"
                },
                new TitleLinkPair
                {
                    Title = "BMW",
                    Link = "http://catalog.mann-filter.com/EU/tur/vehicle/MANN-FILTER%20Katalog%20Europa/Ara%C3%A7lar/Otomobiller%20+%20Ticari%20Ara%C3%A7lar/BMW"
                }
            });
            planFile.AddContent(vehicleTypeProducers);

            planFile.Save("Save_NonEmptyArray_FileExists.pf");

            Assert.IsTrue(File.Exists("Save_NonEmptyArray_FileExists.pf"));
        }

        [TestMethod]
        public void Save_EmptyArray_FileExistsWith648Length()
        {
            PlanFile planFile = new PlanFile();
            Dictionary<string, ITitleLinkPair[]> vehicleTypeProducers = new Dictionary<string, ITitleLinkPair[]>();
            planFile.AddContent(vehicleTypeProducers);
            string fileName = "Save_EmptyArray_FileExistsWithZeroSize.pf";
            planFile.Save(fileName);

            FileInfo f = new FileInfo(fileName);
            Assert.IsTrue(File.Exists(fileName) && f.Length == 648);
        }

        [TestMethod]
        public void Load_File_4Items()
        {
            PlanFile planFile = new PlanFile();
            string fileName = "Save_NonEmptyArray_FileExists.pf";
            planFile.Load<Dictionary<string, ITitleLinkPair[]>>(fileName);
            Dictionary<string, ITitleLinkPair[]> vehicleTypeProducers = planFile.GetContent<Dictionary<string, ITitleLinkPair[]>>();

            Assert.IsTrue(vehicleTypeProducers.Count == 1 && vehicleTypeProducers["Otomobil"].Length == 4);
        }
    }
}
