using System.Net.Http;
using System.Net.Http.Headers;
using Centtrip.Model;
using Centtrip.Helper;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;

namespace Centtrip.API
{
    public enum CallType
    {
        Single,
        List
    }

    public class APIProvider : IAPIProvider
    {
        private readonly ICacheService _cacheService;

        public APIProvider(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        /// <summary>
        /// Calls the Marvel API
        /// </summary>
        /// <param name="callType"> Indicates either getting character by Id or list of characters </param>
        /// <param name="input"> Should be characterId when the type is single or offset of result list when type is List </param>
        /// <returns> A response from API that contains a list of characters or a single character </returns>
        public ReceivedResponse<Character> CallAPI(CallType callType, int input)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var baseURL = ConfigReader.BaseURL;
                switch (callType)
                {
                    case CallType.Single:
                        baseURL += $"/{input}?";
                        break;
                    case CallType.List:
                        baseURL += $"?offset={input}&limit=100&";
                        break;
                }

                var url = $"{baseURL}ts={ConfigReader.TimeStamp}&apikey={ConfigReader.APIPublicKey}&hash={ConfigReader.HashCode}";

                var response = client.GetAsync(url).Result;

                ReceivedResponse<Character> result;
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsAsync<ReceivedResponse<Character>>().Result;
                }
                else
                {
                    //log response status here..
                    var error = new HttpResponseMessage()
                    {
                        Content = new StringContent(response.StatusCode.ToString()),
                        ReasonPhrase = response.ReasonPhrase
                    };
                    throw new HttpResponseException(error);
                }
                return result;
            }
        }
        /// <summary>
        /// Caches the characters list in the memory
        /// </summary>
        /// <returns>List of characters</returns>
        public IEnumerable<Character> GetMarvelCharacters()
        {
            return _cacheService.GetOrSet("marvel.characters", () => RefreshCharacters());
        }

        /// <summary>
        /// Calls the API and gets all the existing characters
        /// </summary>
        /// <returns>List of characters</returns>
        public IEnumerable<Character> RefreshCharacters()
        {
            var responseList = new List<ReceivedResponse<Character>>();
            var response = CallAPI(CallType.List, 0);
            responseList.Add(response);
            var totalRows = response.Data.Total / 100;

            for (int i = 1; i <= totalRows; i++)
            {
                responseList.Add(CallAPI(CallType.List, i * 100));
            }
            return responseList.SelectMany(x => x.Data.Results);
        }
    }
}
