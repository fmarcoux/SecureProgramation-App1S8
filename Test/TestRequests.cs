using CoupDeSonde.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Moq;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using System.Collections.Specialized;
using System.Security.Principal;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
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
            apiController.ControllerContext.HttpContext.Request.Headers["X-API-KEY"] = "secretKey";

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
            apiController.ControllerContext.HttpContext.Request.Headers["X-API-KEY"] = "noKey";

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
            apiController.ControllerContext.HttpContext.Request.Headers["X-API-KEY"] = "noKey";

            SurveyController.Survey survey = new SurveyController.Survey();
            SurveyController.CreateSurvey(survey, 1);

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
            apiController.ControllerContext.HttpContext.Request.Headers["X-API-KEY"] = "secretKey";


            SurveyController.Survey survey = new SurveyController.Survey();
            SurveyController.CreateSurvey(survey, 1);

            var response = apiController.PostSurvey(survey);

            Assert.IsType<OkResult>(response.Result);
        }
    }
}