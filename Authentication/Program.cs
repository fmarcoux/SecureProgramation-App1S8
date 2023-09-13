using System.Data.SQLite;
using System.Net;
using System.Reflection;

const string tokenQueryString = @"SELECT name FROM AuthTable WHERE api_key=""{0}"" ;";

void Main(string[]? args)
{
    try
    {
        var guid = ValidateGuid(args!.FirstOrDefault());
        var connection = OpenDataBase();
        var name = QueryNameFromAPIKeyAndClose(connection, guid!);
        Console.WriteLine(name!);
    }
    catch (Exception)
    {
        Console.WriteLine("ERREUR");
    }

}

string? ValidateGuid(string? guidString)
{
    if (!Guid.TryParse(args[0], out Guid guid))
    {
        throw new Exception("Bad GUID");
    }
    return guid.ToString().ToUpper();
}

SQLiteConnection OpenDataBase()
{
    //La base de données d'authentification doit se trouver toujours dans le même répertoire que l'exe,
    //Ça devrait être géré automatiquement
    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Auth.BD");
    Console.WriteLine(path);
    var connection = new SQLiteConnection(string.Format("Data Source={0}", path));
    connection.Open();
    return connection;
}


string? QueryNameFromAPIKeyAndClose(SQLiteConnection databaseConnection , string guidInUpperCase)
{
    SQLiteDataReader sqlite_datareader; ;
    SQLiteCommand sqlite_cmd;
    sqlite_cmd = databaseConnection.CreateCommand();
    sqlite_cmd.CommandText = string.Format(tokenQueryString, guidInUpperCase);
   
    sqlite_datareader = sqlite_cmd.ExecuteReader();

    if (!sqlite_datareader.Read())
    {
        throw new Exception("No data");
    }
    string name = sqlite_datareader.GetString(0);
    databaseConnection.Close();
    return name;
}

Main(args);



    
