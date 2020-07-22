using System;
using System.Collections.Generic;
using System.Linq;
using DataWebservice.Data;
using DataWebservice.Models;


namespace DataWebservice.Controllers.API
{
    public interface IUserService
    {
        User Authenticate(string username, string password);

    }

    public class UserService : IUserService
    {
        private DataWebserviceContext _context;

        public UserService(DataWebserviceContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.User.SingleOrDefault(x => x.displayName == username);

            // check if username exists
            if (user == null)
                return null;

            if (password != user.password)
                return null;

            // authentication successful
            return user;
        }


    }
}