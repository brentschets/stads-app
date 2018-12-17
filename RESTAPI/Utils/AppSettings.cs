namespace RESTAPI.Utils
{
    public class AppSettings
    {
        public static string Secret { get;set; }
        //local (gebruik deze url voor offline database)
        public const string HostName = "https://localhost:44301";
        //deploy (gebruik deze url voor online database)
        //public const string HostName = "https://stadsapprestapi.azurewebsites.net";
    }
}
