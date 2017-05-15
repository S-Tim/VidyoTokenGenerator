using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TokenGeneratorApi.Controllers;

namespace TokenGeneratorApi.Tests.Controllers
{
    [TestClass]
    public class TokenControllerTest
    {
        [TestMethod]
        public void TestGetShouldReturnToken()
        {
            // Arrange
            TokenController tokenController = new TokenController();
            string username = "user1";
            string expiresAt = "2055-10-27T10:54:22Z";

            // Act
            var result = tokenController.GetToken(username, expiresAt) as OkNegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Content, typeof(string));
        }
    }
}
