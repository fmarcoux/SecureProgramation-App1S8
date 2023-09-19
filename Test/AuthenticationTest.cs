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
            Assert.True(Authentication.AuthenticationManager.ValidateAPIKey("C6A39641-2F1D-45A3-8979-2115FAE82B04") == "frank");
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