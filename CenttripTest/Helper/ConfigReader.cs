using System.Configuration;

namespace Centtrip.Helper
{
    /// <summary>
    /// Reads the data by key from the Web.Config file
    /// </summary>
    public static class ConfigReader
    {
        public static string BaseURL => ConfigurationManager.AppSettings["Marvel_BaseUrl"];
        public static string APIPublicKey => ConfigurationManager.AppSettings["Marvel_PublicKey"];
        public static string APIPrivateKey => ConfigurationManager.AppSettings["Marvel_PrivateKey"];
        public static string TimeStamp => ConfigurationManager.AppSettings["Marvel_TS"];
        // This has been generated online using this formula ts + _apiPrivateKey + _apiPublicKey
        public static string HashCode = ConfigurationManager.AppSettings["Marvel_Hash"];
    }
}