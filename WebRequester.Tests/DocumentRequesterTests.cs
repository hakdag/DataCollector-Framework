using DataCollector.WebRequest.Exceptions;
using DataCollector.WebRequest.Get;
using MannDBCollector.Common.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace WebRequester.Tests
{
    [TestClass]
    public class DocumentRequesterTests
    {
        [TestMethod]
        public void Constructor_EmptryUrl_ThrowInvalidURLException()
        {
            string url = "";
            VehicleTypes type = VehicleTypes.OtomobillerTicariAraclar;
            try
            {
                DocumentRequestor documentRequester = new DocumentRequestor(new WebRequestor(), url);
                Assert.Fail("Request must fail with an empty url given.");
            }
            catch (InvalidURLException) { }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [TestMethod]
        public void Constructor_ValidUrl_NoException()
        {
            string url = "http://catalog.mann-filter.com/EU/tur/vehicle/MANN-FILTER%20Katalog%20Europa/Ara%C3%A7lar/Otomobiller%20%2B%20Ticari%20Ara%C3%A7lar";
            VehicleTypes type = VehicleTypes.OtomobillerTicariAraclar;
            try
            {
                DocumentRequestor documentRequester = new DocumentRequestor(new WebRequestor(), url);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [TestMethod]
        public void Constructor_ValidUrlWithNoType_NoException()
        {
            string url = "http://catalog.mann-filter.com/EU/tur/vehicle/MANN-FILTER%20Katalog%20Europa/Ara%C3%A7lar/Otomobiller%20%2B%20Ticari%20Ara%C3%A7lar";
            try
            {
                DocumentRequestor documentRequester = new DocumentRequestor(new WebRequestor(), url);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }
    }
}
