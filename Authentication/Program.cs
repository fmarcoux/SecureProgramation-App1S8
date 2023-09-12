using System.Data.SQLite;
using System.Reflection;

const string tokenQueryString = @"SELECT name FROM AuthTable WHERE api_key=""{0}"" ;";

try
{
    if (!Guid.TryParse(args[0], out Guid guid))
    {
        throw new Exception("Bad GUID");
    }

    //La base de données d'authentification doit se trouver toujours dans le même répertoire que l'exe,
    //Ça devrait être géré automatiquement
    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Auth.BD");
    Console.WriteLine(path);
    var connection = new SQLiteConnection(string.Format("Data Source={0}", path));
    connection.Open();

    SQLiteDataReader sqlite_datareader;
    SQLiteCommand sqlite_cmd;
    sqlite_cmd = connection.CreateCommand();
    sqlite_cmd.CommandText = string.Format(tokenQueryString, guid.ToString().ToUpper());
    Console.WriteLine(guid);
    sqlite_datareader = sqlite_cmd.ExecuteReader();

    if (!sqlite_datareader.Read())
    {
        throw new Exception("No data");
    }
    string myreader = sqlite_datareader.GetString(0);
    Console.WriteLine(myreader);
    connection.Close();
}
catch (Exception)
{    
    Console.WriteLine("ERREUR");
}




    
