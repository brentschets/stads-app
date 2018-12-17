namespace RESTAPI.Utils
{
    public class AppSettings
    {
        public static string Secret { get;set; }
        //local
        //public const string HostName = "https://localhost:44301";
        //deploy
        public const string HostName = "https://stadsapprestapi.azurewebsites.net";
    }
}
