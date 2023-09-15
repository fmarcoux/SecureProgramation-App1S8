namespace Test
{
    public class AuthenticationTest
    {
        
        [Fact]
        public void TestAuthenticationManager_APIKeyNotFoundInDatabaseShouldReturnERREUR()
        {
            Assert.True(Authentication.AuthenticationManager.ValidateAPIKey("test") == "ERREUR");
        }

        [Fact]
        public void TestAuthenticationManager_ValidAPIKeyShouldReturnValidName()
        {
            Assert.True(Authentication.AuthenticationManager.ValidateAPIKey("4FB22B8A-61D4-47DB-AAAC-8FB7F133491F") == "gwenn");
        }

        [Fact]
        public void TestAuthenticationManager_nullApiKeyShouldReturnERREUR()
        {
            Assert.True(Authentication.AuthenticationManager.ValidateAPIKey(null) == "ERREUR");
        }

        [Fact]
        public void TestAuthenticationManager_ValidGuidButtNotPresentInDBShouldReturnERREUR()
        {
            Assert.True(Authentication.AuthenticationManager.ValidateAPIKey("F2568220-6D91-4790-8695-EC5AEA7D3F86") == "ERREUR");
        }

    }
}