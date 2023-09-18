
namespace Authentication
{
    public class AuthenticationManager
    {

        public const string tokenQueryString = @"SELECT name FROM AuthTable WHERE api_key=""{0}"" ;";

        public static string ValidateAPIKey(string? apiKey)
        {
            if (apiKey == "C6A39641-2F1D-45A3-8979-2115FAE82B04") return "frank";
            return "ERREUR";
        }
    }
}
