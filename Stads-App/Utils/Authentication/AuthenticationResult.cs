using Stads_App.Models;

namespace Stads_App.Utils.Authentication
{
    public class AuthenticationResult
    {
        public bool Success { get; internal set; }
        public AuthenticationError Error { get; internal set; }
        public User User { get; internal set; }
    }
}
