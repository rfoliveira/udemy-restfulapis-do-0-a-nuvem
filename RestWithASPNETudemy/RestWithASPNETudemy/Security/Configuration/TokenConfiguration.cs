namespace RestWithASPNETudemy.Security.Configuration
{
    public class TokenConfiguration
    {
        public bool Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }
}
