using DataCollector.Common.Contracts;
using DataCollector.WebRequest;
using DataCollector.WebRequest.Get;
using HtmlAgilityPack;
using MannDBCollector;
using MannDBCollector.Common.Contracts;
using MannDBCollector.Requestors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace WebRequester.Tests
{
    /// <summary>
    /// Summary description for ExecutionPlanTests
    /// </summary>
    [TestClass]
    public class ExecutionPlanTests
    {
        public ExecutionPlanTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
        }

        [ClassCleanup()]
        public static void MyClassCleanup() { }
        #endregion
        /*
        [TestMethod]
        public void CreatePlan_NoTypeGiven_VehicleTypesLengthEquals4()
        {
            try
            {
                //Arrange
                IInitialPageDocumentRequestor drInitialPage = createInitialPageDRObj();
                string vehicleType1Url = "http://catalog.mann-filter.com/EU/tur/vehicle/MANN-FILTER%20Katalog%20Europa/Ara%C3%A7lar/Otomobiller%20%2B%20Ticari%20Ara%C3%A7lar";
                string vehicleType2Url = "http://catalog.mann-filter.com/EU/tur/vehicle/MANN-FILTER%20Katalog%20Europa/Ara%C3%A7lar/Kamyon%20%2B%20Otob%C3%BCs";
                string vehicleType3Url = "http://catalog.mann-filter.com/EU/tur/vehicle/MANN-FILTER%20Katalog%20Europa/Ara%C3%A7lar/Off%20Highway%20uygulamalar%C4%B1";
                string vehicleType4Url = "http://catalog.mann-filter.com/EU/tur/vehicle/MANN-FILTER%20Katalog%20Europa/Ara%C3%A7lar/Motosiklet";
                IVehicleTypeDocumentRequestor drVehicleType1 = createVehicleTypeDRObj(vehicleType1Url, Resources.MANNFILTER_Araclar_Otomobiller_Ticari_Araclar);
                IVehicleTypeDocumentRequestor drVehicleType2 = createVehicleTypeDRObj(vehicleType2Url, Resources.MANNFILTER_Araclar_Kamyon_Otobus);
                IVehicleTypeDocumentRequestor drVehicleType3 = createVehicleTypeDRObj(vehicleType3Url, Resources.MANNFILTER_Araclar_Off_Highway_uygulamalari);
                IVehicleTypeDocumentRequestor drVehicleType4 = createVehicleTypeDRObj(vehicleType4Url, Resources.MANNFILTER_Araclar_Motosiklet);

                //Act
                var executionPlan = new MannDBExecutionPlan(drInitialPage, new[]{ "Kategori", "Üretici", "Model Adı", "Tipi", "Motor Kodu", "kW", "PS", "Yıl", "Hava Filtresi", "Ölçü Hava", "Yağ Filtresi", "Ölçü Yağ", "Benzin Filtresi", "Ölçü Benzin", "İç Mekan Filtresi", "Ölçü İç Mekan", "Diğer Filtreler", "Ölçü Diğer" });
                executionPlan.CreatePlan();
                executionPlan.ExecutePlan();

                //Assert
                Assert.IsTrue(executionPlan.VehicleTypes.Length == 4);
                Assert.IsTrue(executionPlan.VehicleTypeProducers.Count == 4);
                Assert.IsTrue(executionPlan.VehicleTypeProducers["Otomobiller + Ticari Araçlar"].Length == 123);
                Assert.IsTrue(executionPlan.VehicleTypeProducers["Kamyon + Otobüs"].Length == 129);
                Assert.IsTrue(executionPlan.VehicleTypeProducers["Off Highway uygulamaları"].Length == 404);
                Assert.IsTrue(executionPlan.VehicleTypeProducers["Motosiklet"].Length == 48);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }
        */
        private IInitialPageDocumentRequestor createInitialPageDRObj()
        {
            string initialPageUrl = "http://catalog.mann-filter.com/EU/tur/vehicle/MANN-FILTER%20Katalog%20Europa/Ara%C3%A7lar";
            Mock<InitialPageDocumentRequestor> mockDocumentRequester = new Mock<InitialPageDocumentRequestor>(initialPageUrl);
            WebRequestor webRequestor = new WebRequestor();
            HtmlDocument initialPage = webRequestor.CreateHtmlDocument(Resources.mannfilter_initial_page);
            mockDocumentRequester.Setup(dr => dr.GetHtml()).Returns(initialPage);
            IInitialPageDocumentRequestor documentRequester = mockDocumentRequester.Object;
            return documentRequester;
        }

        private IVehicleTypeDocumentRequestor createVehicleTypeDRObj(string vehicleTypeUrl, string content)
        {
            Mock<VehicleTypeDocumentRequestor> mockDRVehicleType = new Mock<VehicleTypeDocumentRequestor>(vehicleTypeUrl);
            WebRequestor webRequestor = new WebRequestor();
            HtmlDocument vehicleTypePage = webRequestor.CreateHtmlDocument(content);
            mockDRVehicleType.Setup(dr => dr.GetHtml()).Returns(vehicleTypePage);
            IVehicleTypeDocumentRequestor documentRequester = mockDRVehicleType.Object;
            return documentRequester;
        }
    }
}
