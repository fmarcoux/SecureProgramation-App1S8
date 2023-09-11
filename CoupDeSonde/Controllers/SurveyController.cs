using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Data;

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
        }
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> GetSurvey()
        //{

        //}

        [HttpPost("postSondage")]
        public ActionResult<string> PostSurvey([FromBody] Survey survey)
        {
            //if (HttpContext.Request.Headers.ContainsKey("X-API-KEY"))
            //{
            //  var apiKey = HttpContext.Request.Headers["X-API-KEY"];

            //should check whether the key is good or not

            Console.WriteLine("Request received");
            string dbPath = Path.Combine(Environment.CurrentDirectory, "surveyResults.db");
            string connString = string.Format("Data Source={0}", dbPath);
            var connection = new SqliteConnection(connString);
            connection.Open();
            Console.WriteLine("Connection to DB established");

            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = String.Format($"INSERT INTO resultats(SurveyID,Answer)VALUES({survey.SurveyNumber},'{survey.Answers}')");

            sqlite_cmd.ExecuteReader();

            return "done";
            //}
            //else
            //{
              //  return "bad key";
            //}

        }
    }
}