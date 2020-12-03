using Centtrip.API;
using Newtonsoft.Json;
using System.Linq;
using System.Web.Http;

namespace Centtrip.Controllers
{
    public class CharacterController : ApiController
    {
        private readonly IAPIProvider _apiProvider;
        public CharacterController(IAPIProvider apiProvider)
        {
            _apiProvider = apiProvider;
        }

        //GET /characters
        [HttpGet]
        [Route("characters")]
        public IHttpActionResult Characters()
        {
            var allCharacterIds = _apiProvider.GetMarvelCharacters().Select(x => x.Id);
            return Json(allCharacterIds);
        }

        // GET /characters/1010973
        [HttpGet]
        [Route("characters/{id}")]
        public IHttpActionResult Character(int id)
        {
            var character = _apiProvider.CallAPI(CallType.Single, id).Data.Results.SingleOrDefault();
            var options = new JsonSerializerSettings() { Formatting = Formatting.Indented };
            return Json(character, options);
        }

    }
}
