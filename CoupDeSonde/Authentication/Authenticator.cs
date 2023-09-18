using System.Diagnostics;
using System.Reflection;

namespace CoupDeSonde.Authentication
{

    public class Authenticator : Authentification
    {
        private readonly string m_authenticatorServicePath ;
        private readonly string m_authenticatorServiceName;
        
        public Authenticator()
        {
            m_authenticatorServiceName = "Authentication.exe";
            m_authenticatorServicePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, m_authenticatorServiceName);
        }

        public string ServicePathGet()
        {
            return m_authenticatorServicePath;
        }
        
        public string Authenticate(string ? guid)
        {
            string authenticationResult="ERREUR";
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = m_authenticatorServicePath,
                    Arguments = guid,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
            proc.Start();
            while (!proc.StandardOutput.EndOfStream)
            {
                var test = proc.StandardOutput.ReadLine();
                return "frank";
                //authenticationResult = proc.StandardOutput.ReadLine()!;
            }

            return authenticationResult!;
        }
    }
}
