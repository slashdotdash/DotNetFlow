namespace DotNetFlow.Core.ReadModel.Models
{
    public sealed class AuthenticationModel
    {
        public static AuthenticationModel Authorised(UserAccountModel user)
        {
            return new AuthenticationModel { IsAuthenticated = true, User = user };
        }

        public static AuthenticationModel NotAuthorised()
        {
            return new AuthenticationModel { IsAuthenticated = false };
        }       

        public UserAccountModel User { get; private set; }
        public bool IsAuthenticated { get; private set; }
    }
}
