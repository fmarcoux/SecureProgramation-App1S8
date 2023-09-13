﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace CoupDeSonde.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class SurveyController : ControllerBase
    {
        public class Survey
        {
            //there are two possible surveys: 1 and 2
            public int SurveyNumber { get; set; }
            //the answers are concatenated under a string form: 'abcd'
            public string Answers { get; set; }
            //the questions of the survey depend on the SurveyNumber
            public string Question1 { get; set; }
            public string Question2 { get; set; }
            public string Question3 { get; set; }
            public string Question4 { get; set; }
            
            public Survey(int number)
            {
                SurveyNumber = number;
                Answers = "";
                Question1 = "1. À quelle tranche d'âge appartenez-vous? a:0-25 ans, b:25-50 ans, c:50-75 ans, d:75 ans et plus\n\n";
                Question2 = "2. Êtes-vous une femme ou un homme? a:Femme, b:Homme, c:Je ne veux pas répondre\n\n";
                
                if (SurveyNumber == 1)
                {
                    Question3 = "3. Quel journal lisez-vous à la maison? a:La Presse, b:Le Journal de Montréal, c:The Gazette, d:Le Devoir\n\n";
                    Question4 = "4. Combien de temps accordez-vous à la lecture de votre journal quotidiennement? a:Moins de 10 minutes; b:Entre 10 et 30 minutes, c:Entre 30 et 60 minutes, d:60 minutes ou plus\n\n";
                }
                else
                {
                    Question3 = "3. Combien de tasses de café buvez-vous chaque jour? a: Je ne bois pas de café, b:Entre 1 et 5 tasses, c: Entre 6 et 10 tasses, d: 10 tasses ou plus\n\n";
                    Question4 = "4. Combien de consommations alcoolisées buvez-vous chaque jour? a: 0, b: 1, c: 2 ou 3, d: 3 ou plus\n\n";
                }
            }
        }
        /*
        static string checkKey(string key)
        {
            
            string pathToExe = "Path.exe";

            Process process = new Process();
            process.StartInfo.FileName = "Path.exe";
            process.StartInfo.Arguments = key;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();

            //read the output of the key checker
            string output = process.StandardOutput.ReadToEnd();

            return output;
        }
        */
        
        [HttpGet("sondage")]
        public ActionResult<IEnumerable<string>> GetSurvey()
        {
            //select and create randomly one of the two surveys
            Random rand = new Random();
            int surveyNumber = rand.Next(1, 3);
            Console.WriteLine($"Submitting survey number {surveyNumber}");

            Survey requested = new Survey(surveyNumber);
            return Ok(requested);
        }
        
        
        [HttpPost("sondage")]
        public ActionResult<string> PostSurvey([FromBody] Survey survey)
        {/*
            if (HttpContext.Request.Headers.ContainsKey("X-API-KEY"))
            {
                string apiKey = HttpContext.Request.Headers["X-API-KEY"];

                //should check whether the key is good or not
                string username = SurveyController.checkKey(apiKey);

                if (username != "ERR")
                {*/
            Console.WriteLine("Request received");

            string dbPath = Path.Combine(Environment.CurrentDirectory, "surveyResults.db");
            string connString = string.Format("Data Source={0}", dbPath);
            var connection = new SqliteConnection(connString);
            connection.Open();
            Console.WriteLine("Connection to DB established");

            //SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = String.Format($"INSERT INTO resultats(SurveyID,Answer)VALUES({survey.SurveyNumber},'{survey.Answers}')");

            sqlite_cmd.ExecuteReader();

            return Ok();/*
                }
                else
                {
                    return StatusCode(401);
                }

            }
            else
            {
                return BadRequest();
            }*/

        }

        [Route("/error")]
        public IActionResult HandleError() =>
            Problem();
    }
}