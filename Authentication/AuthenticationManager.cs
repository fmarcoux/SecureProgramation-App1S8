﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.Net;


namespace Authentication
{
    public class AuthenticationManager
    {
        private readonly string? m_guidString;
        private readonly string m_databaseName;
        private SqliteConnection? m_connection;

        public const string tokenQueryString = @"SELECT name FROM AuthTable WHERE api_key=""{0}"" ;";

        private AuthenticationManager(string? guidString)
        {
            m_guidString = guidString;
            m_databaseName = "Auth.BD";
        }

        private string? ValidateGuid()
        {
            if (!Guid.TryParse(m_guidString!, out Guid guid))
            {
                throw new Exception("Bad GUID");
            }
            return guid.ToString().ToUpper();
        }

        private void OpenDataBase()
        {
            //La base de données d'authentification doit se trouver toujours dans le même répertoire que l'exe,
            //Ça devrait être géré automatiquement
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, m_databaseName);            
            m_connection = new SqliteConnection(string.Format("Data Source={0}", path));
            m_connection.Open();
        }

        private string QueryNameFromAPIKeyAndClose(string guidInUpperCase)
        {

            SqliteCommand sqlite_cmd;
            sqlite_cmd = m_connection!.CreateCommand();
            sqlite_cmd.CommandText = String.Format($"Select * from resultats");
            var sqlite_datareader = sqlite_cmd.ExecuteReader(); 
            
            if (!sqlite_datareader.Read() )
            {
                throw new Exception("No row matching"); 
            }
            string name = sqlite_datareader.GetString(0);
            m_connection.Close();
            return name;
        }
        
        public static string ValidateAPIKey(string? apiKey)
        {
            var authenticationManager = new AuthenticationManager(apiKey);
            try
            {
                var guid = authenticationManager.ValidateGuid();
                authenticationManager.OpenDataBase();
                return authenticationManager.QueryNameFromAPIKeyAndClose(guid!);
            }
            catch (Exception e) 
            {
                return e.Message;
            }
        }
    }
}
