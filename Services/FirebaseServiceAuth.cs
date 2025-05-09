using Firebase.Auth;

namespace Mobappg4v2.Services
{
    public class FirebaseAuthService
    {
        private const string ApiKey = "AIzaSyB9HyEMCLO9-topKiBzpLmizD8VODMzBiA";
        private readonly FirebaseAuthProvider authProvider;

        public FirebaseAuthService()
        {
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
        }

        public async Task<FirebaseAuthLink> SignUp(string email, string password)
        {
            return await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
        }

        public async Task<FirebaseAuthLink> SignIn(string email, string password)
        {
            return await authProvider.SignInWithEmailAndPasswordAsync(email, password);
        }

        public async Task<string> GetFreshToken(FirebaseAuthLink authLink)
        {
            await authLink.GetFreshAuthAsync();
            return authLink.FirebaseToken;
        }
    }
}