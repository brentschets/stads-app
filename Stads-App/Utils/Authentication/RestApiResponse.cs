using Stads_App.Models;

namespace Stads_App.Utils.Authentication
{
    public class RestApiResponse
    {
        public bool Success { get; internal set; }
        public RestApiError Error { get; internal set; }
        public User User { get; internal set; }
    }

    public class RestApiError
    {
        public string Message { get; set; }
    }
}
