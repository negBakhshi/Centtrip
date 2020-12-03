namespace Centtrip.Model
{
    /// <summary>
    /// This Class has been used for storing each Character's information
    /// </summary>
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Modified { get; set; }
        public Thumbnail Thumbnail { get; set; }
    }
}

