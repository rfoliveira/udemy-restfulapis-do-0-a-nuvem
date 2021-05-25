namespace RestWithASPNETUdemy.Data.VO
{
    public class TokenVO
    {
        public bool Authenticated { get; set; }
        public string CreatedExpiration { get; set; }
        public string Expiration { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public TokenVO(bool authenticated, string createdExpiration, string expiration, string accessToken, string refreshToken)
        {
            Authenticated = authenticated;
            CreatedExpiration = createdExpiration;
            Expiration = expiration;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}