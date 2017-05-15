using System.Web.Http;
using System.Web.Http.Description;
using VidyoTokenGenerator;

namespace TokenGeneratorApi.Controllers
{
    public class TokenController : ApiController
    {
        private string key = "rUlaMASgt1Byi4Kp3sKYDeQzo";
        private string appId = "ApplicationID";

        // GET : api/token
        [ResponseType(typeof(string))]
        public IHttpActionResult GetToken(string username, string expiresAt)
        {
            return Ok(TokenGenerator.GenerateToken(key, appId, username, 0, expiresAt));
        }

        // POST : api/token
        // Hide username in request body
        public IHttpActionResult PostToken([FromBody]string username, string expiresAt)
        {
            return Ok(TokenGenerator.GenerateToken(key, appId, username, 0, expiresAt));
        }
    }
}