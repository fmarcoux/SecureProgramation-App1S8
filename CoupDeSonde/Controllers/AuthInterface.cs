using System;

namespace CoupDeSonde
{
	public interface AuthInterface
	{
		string authenticate(string guid);
	}

    public class Authenticator : AuthInterface
    {
        public string authenticate(string guid)
        {
            if (guid == "secretKey")
            {
                return "userTest";
            }
            else
            {
                return "ERR";
            }
        }
    }
}