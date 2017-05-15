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
            Assert.AreEqual(result.Content, "cHJvdmlzaW9uAHVzZXIxQEFwcGxpY2F0aW9uSUQANjQ4NzU0NjY0NjIAADA5YTdkNDMwMTI3NzlkOWQzZTg5YzRlMjg5ZWUwNGU0MWRlOTBhYjI3ZDlhYzdlYmU3MGZhMDY0MzQ4Nzc4ZDMwNDhjMGE3MjhlZThiNDkwMDMxOGUxZWRjNjhkY2NjZA==");
        }
    }
}
