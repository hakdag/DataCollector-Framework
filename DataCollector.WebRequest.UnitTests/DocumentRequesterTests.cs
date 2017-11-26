using DataCollector.Common.Contracts;
using DataCollector.WebRequest.Exceptions;
using DataCollector.WebRequest.Get;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DataCollector.WebRequest.UnitTests
{
    [TestClass]
    public class DocumentRequesterTests
    {
        [TestMethod]
        public void GetHtml_PersistCode200WhileCode301_ThrowPersistStatusCode200Exception()
        {
            //Arrange
            Mock<IWebRequestor> mockWebRequestor = new Mock<IWebRequestor>();

            //Act
            mockWebRequestor.Setup(wr => wr.GetResponseHtml("www.test.com", null, true)).Throws<PersistStatusCode200Exception>();
            DocumentRequestor documentRequester = new DocumentRequestor(mockWebRequestor.Object, "www.test.com", true);
            HtmlDocument doc = documentRequester.GetHtml();

            //Assert
            Assert.IsTrue(doc == null && documentRequester.State == Common.RequestStates.Failed);
        }
    }
}
