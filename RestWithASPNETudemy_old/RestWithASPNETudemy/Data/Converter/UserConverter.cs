using RestWithASPNETudemy.Data.VO;
using RestWithASPNETudemy.Models;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNETudemy.Data.Converter
{
    public class UserConverter : IParser<UserVO, User>, IParser<User, UserVO>
    {
        public User Parse(UserVO origin)
        {
            if (origin == null) return new User();

            return new User
            {
                Id = origin.Id,
                Login = origin.Login,
                AcceessKey = origin.AcceessKey
            };
        }

        public UserVO Parse(User origin)
        {
            if (origin == null) return new UserVO();

            return new UserVO
            {
                Id = origin.Id,
                Login = origin.Login,
                AcceessKey = origin.AcceessKey
            };
        }

        public List<User> ParseList(List<UserVO> origin)
        {
            if (origin == null) return new List<User>();

            return origin.Select(usr => Parse(usr)).ToList();
        }

        public List<UserVO> ParseList(List<User> origin)
        {
            if (origin == null) return new List<UserVO>();

            return origin.Select(usr => Parse(usr)).ToList();
        }
    }
}
