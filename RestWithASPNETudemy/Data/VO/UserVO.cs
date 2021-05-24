using System;

namespace RestWithASPNETUdemy.Data.VO
{
    public class UserVO
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}