namespace Bill.Domain.Responses
{
    public class AuthenticationUserResponse
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpirationTime { get; set; }
    }
}