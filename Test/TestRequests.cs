using CoupDeSonde.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.AspNetCore.Http;
using CoupDeSonde.Authentication;

namespace Test
{
   
    public class TestRequests
    {
        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        // create controller object and modify its httpcontest 
        [Fact]
        public void TestGetRequest_ValidAPIKeyShouldReturn200()
        {
            var AuthenticatorMoq = new Mock<Authentification>();
            AuthenticatorMoq.Setup(mock => mock.Authenticate(It.IsAny<string>())).Returns("someValidKey");

            SurveyController apiController = new SurveyController();
            apiController.SetAuthenticator(AuthenticatorMoq.Object);

            apiController.ControllerContext = new ControllerContext();
            apiController.ControllerContext.HttpContext = new DefaultHttpContext();
            apiController.ControllerContext.HttpContext.Request.Headers["X-API-KEY"] = "4044AA1F-CAEE-42FC-BC17-C01E8720F81A";

            var response = apiController.GetSurvey();

            Assert.IsType<OkObjectResult>(response.Result);
            
        }

        [Fact]
        public void TestGetRequest_InvalidAPIKeyShouldReturn401()
        {

            var AuthenticatorMoq = new Mock<Authentification>();
            AuthenticatorMoq.Setup(mock => mock.Authenticate(It.IsAny<string>())).Returns("ERREUR");

            SurveyController apiController = new SurveyController();
            apiController.SetAuthenticator(AuthenticatorMoq.Object);
            apiController.ControllerContext = new ControllerContext();
            apiController.ControllerContext.HttpContext = new DefaultHttpContext();
            apiController.ControllerContext.HttpContext.Request.Headers["X-API-KEY"] = "4044AA1F-CAEE-42FC-BC17-C01E8720F81A";

            var response = apiController.GetSurvey();

            Assert.IsType<UnauthorizedResult>(response.Result);

        }

        [Fact]
        public void TestGetRequest_noAPIKeyShouldReturn400()
        {

            var AuthenticatorMoq = new Mock<Authentification>();
            AuthenticatorMoq.Setup(mock => mock.Authenticate(It.IsAny<string>())).Returns("someApiKey");

            SurveyController apiController = new SurveyController();
            apiController.SetAuthenticator(AuthenticatorMoq.Object);

            apiController.ControllerContext = new ControllerContext();
            apiController.ControllerContext.HttpContext = new DefaultHttpContext();
           

            var response = apiController.GetSurvey();

            Assert.IsType<BadRequestResult>(response.Result);

        }
        
        [Fact]
        public void TestPostRequest_InvalidAPIKeyShouldReturn401()
        {
            var AuthenticatorMoq = new Mock<Authentification>();
            AuthenticatorMoq.Setup(mock => mock.Authenticate(It.IsAny<string>())).Returns("ERREUR");

            SurveyController apiController = new SurveyController();
            apiController.SetAuthenticator(AuthenticatorMoq.Object);

            apiController.ControllerContext = new ControllerContext();
            apiController.ControllerContext.HttpContext = new DefaultHttpContext();
            apiController.ControllerContext.HttpContext.Request.Headers["X-API-KEY"] = "4044AA1F-CAEE-42FC-BC17-C01E8720F81A";

            SurveyController.Survey survey = new SurveyController.Survey();
            SurveyController.CreateSurvey(survey, 1);
            survey.Answers = "abcd";

            var response = apiController.PostSurvey(survey);

            Assert.IsType<UnauthorizedResult>(response.Result);
        }

        [Fact]
        public void TestPostRequest_noAPIKeyShouldReturn400()
        {

            var AuthenticatorMoq = new Mock<Authentification>();
            AuthenticatorMoq.Setup(mock => mock.Authenticate(It.IsAny<string>())).Returns("someValidApiKey");

            SurveyController apiController = new SurveyController();
            apiController.SetAuthenticator(AuthenticatorMoq.Object);

            apiController.ControllerContext = new ControllerContext();
            apiController.ControllerContext.HttpContext = new DefaultHttpContext();

            SurveyController.Survey survey = new SurveyController.Survey();
            SurveyController.CreateSurvey(survey, 1);

            var response = apiController.PostSurvey(survey);

            Assert.IsType<BadRequestResult>(response.Result);
        }

        [Fact]
        public void TestPostRequest_ValidAPIKeyShouldReturn200()
        {
            var AuthenticatorMoq = new Mock<Authentification>();
            AuthenticatorMoq.Setup(mock => mock.Authenticate(It.IsAny<string>())).Returns("someValidApiKey");

            SurveyController apiController = new SurveyController();
            apiController.SetAuthenticator(AuthenticatorMoq.Object);

            apiController.ControllerContext = new ControllerContext();
            apiController.ControllerContext.HttpContext = new DefaultHttpContext();
            apiController.ControllerContext.HttpContext.Request.Headers["X-API-KEY"] = "4044AA1F-CAEE-42FC-BC17-C01E8720F81A";


            SurveyController.Survey survey = new SurveyController.Survey();
            SurveyController.CreateSurvey(survey, 1);
            survey.Answers = "abcd";

            var response = apiController.PostSurvey(survey);

            Assert.IsType<OkResult>(response.Result);
        }

        [Fact]
        public void TestPostRequest_APIKeyButNotRightFormatShouldReturnBadRequest()
        {
            var AuthenticatorMoq = new Mock<Authentification>();
            AuthenticatorMoq.Setup(mock => mock.Authenticate(It.IsAny<string>())).Returns("someValidApiKey");

            SurveyController apiController = new SurveyController();
            apiController.SetAuthenticator(AuthenticatorMoq.Object);

            apiController.ControllerContext = new ControllerContext();
            apiController.ControllerContext.HttpContext = new DefaultHttpContext();
            apiController.ControllerContext.HttpContext.Request.Headers["X-API-KEY"] = "apiKeyMalFormatte";


            SurveyController.Survey survey = new SurveyController.Survey();
            SurveyController.CreateSurvey(survey, 1);
            survey.Answers = "abcd";

            var response = apiController.PostSurvey(survey);

            Assert.IsType<BadRequestResult>(response.Result);
        }

        [Fact]
        public void TestPostRequest_ValidAPIKeyButInvalidSurveyAnswerShouldReturnBadRequest()
        {
            var AuthenticatorMoq = new Mock<Authentification>();
            AuthenticatorMoq.Setup(mock => mock.Authenticate(It.IsAny<string>())).Returns("someValidApiKey");

            SurveyController apiController = new SurveyController();
            apiController.SetAuthenticator(AuthenticatorMoq.Object);

            apiController.ControllerContext = new ControllerContext();
            apiController.ControllerContext.HttpContext = new DefaultHttpContext();
            apiController.ControllerContext.HttpContext.Request.Headers["X-API-KEY"] = "4044AA1F-CAEE-42FC-BC17-C01E8720F81A";


            SurveyController.Survey survey = new SurveyController.Survey();
            SurveyController.CreateSurvey(survey, 2);
            survey.Answers = "abcdefg"; //Longue réponse qui n'est pas acceptée

            var response = apiController.PostSurvey(survey);

            Assert.IsType<BadRequestResult>(response.Result);
        }

        [Fact]
        public void TestGetRequestAll_ValidAPIKeyShouldReturn200()
        {
            var AuthenticatorMoq = new Mock<Authentification>();
            AuthenticatorMoq.Setup(mock => mock.Authenticate(It.IsAny<string>())).Returns("someValidKey");

            SurveyController apiController = new SurveyController();
            apiController.SetAuthenticator(AuthenticatorMoq.Object);

            apiController.ControllerContext = new ControllerContext();
            apiController.ControllerContext.HttpContext = new DefaultHttpContext();
            apiController.ControllerContext.HttpContext.Request.Headers["X-API-KEY"] = "4044AA1F-CAEE-42FC-BC17-C01E8720F81A";

            var response = apiController.GetAllSurvey();

            Assert.IsType<OkObjectResult>(response.Result);

        }

        [Fact]
        public void TestGetRequestAll_InvalidAPIKeyShouldReturn401()
        {

            var AuthenticatorMoq = new Mock<Authentification>();
            AuthenticatorMoq.Setup(mock => mock.Authenticate(It.IsAny<string>())).Returns("ERREUR");

            SurveyController apiController = new SurveyController();
            apiController.SetAuthenticator(AuthenticatorMoq.Object);
            apiController.ControllerContext = new ControllerContext();
            apiController.ControllerContext.HttpContext = new DefaultHttpContext();
            apiController.ControllerContext.HttpContext.Request.Headers["X-API-KEY"] = "4044AA1F-CAEE-42FC-BC17-C01E8720F81A";

            var response = apiController.GetAllSurvey();

            Assert.IsType<UnauthorizedResult>(response.Result);

        }

        [Fact]
        public void TestGetAllRequest_noAPIKeyShouldReturn400()
        {

            var AuthenticatorMoq = new Mock<Authentification>();
            AuthenticatorMoq.Setup(mock => mock.Authenticate(It.IsAny<string>())).Returns("someApiKey");

            SurveyController apiController = new SurveyController();
            apiController.SetAuthenticator(AuthenticatorMoq.Object);

            apiController.ControllerContext = new ControllerContext();
            apiController.ControllerContext.HttpContext = new DefaultHttpContext();


            var response = apiController.GetAllSurvey();

            Assert.IsType<BadRequestResult>(response.Result);

        }



        [Fact]
        public void TestGetAllRequestTest_noAPIKeyShouldReturn400()
        {

            var AuthenticatorMoq = new Mock<Authentification>();
            AuthenticatorMoq.Setup(mock => mock.Authenticate(It.IsAny<string>())).Returns("ERREUR");

            SurveyController apiController = new SurveyController();
            apiController.SetAuthenticator(AuthenticatorMoq.Object);

            apiController.ControllerContext = new ControllerContext();
            apiController.ControllerContext.HttpContext = new DefaultHttpContext();
            apiController.ControllerContext.HttpContext.Request.Headers["X-API-KEY"] = "4044AAE-42FCBC17-C01E8720F81A";

            var response = apiController.GetSurvey();
            var response2 = apiController.GetAllSurvey();

            Assert.IsType<BadRequestResult>(response.Result);

        }
    }
}