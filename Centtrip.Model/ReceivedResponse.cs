namespace Centtrip.Model
{
    /// <summary>
    /// This class will be used for storing result of Get request to the Marvel API
    /// </summary>
    /// <typeparam name="T"> Can be each Marvel's entities like: Characters, Comics, Creators, Events </typeparam>
    public class ReceivedResponse<T>
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Copyright { get; set; }
        public string AttributionText { get; set; }
        public string AttributionHTML { get; set; }
        public string Etag { get; set; }
        public ReceivedData<T> Data { get; set; }
    }
}
