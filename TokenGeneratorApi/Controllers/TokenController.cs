using System.Web.Http;
using System.Web.Http.Description;
using VidyoTokenGenerator;

namespace TokenGeneratorApi.Controllers
{
    public class TokenController : ApiController
    {
        [ResponseType(typeof(string))]
        public IHttpActionResult GetToken(string key, string appId, string username, string expiresAt)
        {
            return Ok(TokenGenerator.GenerateToken(key, appId, username, 0, expiresAt));
        }
    }
}