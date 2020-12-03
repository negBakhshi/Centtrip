using Centtrip.Model;
using System.Collections.Generic;

namespace Centtrip.API
{
    public interface IAPIProvider
    {
        ReceivedResponse<Character> CallAPI(CallType callType, int input);
        IEnumerable<Character> RefreshCharacters();
        IEnumerable<Character> GetMarvelCharacters();
    }
}
