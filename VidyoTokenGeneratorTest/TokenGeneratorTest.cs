using Microsoft.VisualStudio.TestTools.UnitTesting;
using VidyoTokenGenerator;

namespace VidyoTokenGeneratorTest
{
    [TestClass]
    public class TokenGeneratorTest
    {
        [TestMethod]
        public void GenerateTokenTest()
        {
            string key = "rUlaMASgt1Byi4Kp3sKYDeQzo";
            string appId = "ApplicationID";
            string username = "user1";
            string expiresAt = "2055-10-27T10:54:22Z";

            TokenGenerator tg = new TokenGenerator(key, appId, username, expiresAt);

            string token = tg.GenerateToken();

            string expected = "cHJvdmlzaW9uAHVzZXIxQEFwcGxpY2F0aW9uSUQANjQ4NzU0NjY0NjIAADA5YTdkNDMwMTI3NzlkOWQzZTg5YzRlMjg5ZWUwNGU0MWRlOTBhYjI3ZDlhYzdlYmU3MGZhMDY0MzQ4Nzc4ZDMwNDhjMGE3MjhlZThiNDkwMDMxOGUxZWRjNjhkY2NjZA==";       
            Assert.AreEqual(expected, token);
        }

    }
}
