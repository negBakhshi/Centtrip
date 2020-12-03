using System.Collections.Generic;

namespace Centtrip.Model
{
    /// <summary>
    /// This class will be used for mapping the received Data inside the response of Get request to the Marvel API
    /// </summary>
    /// <typeparam name="T"> can be each Marvel's entities like: Characters, Comics, Creators, Events </typeparam>
    public class ReceivedData<T>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }
        public List<T> Results { get; set; }
    }
}
