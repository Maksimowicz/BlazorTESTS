using BlazorTEST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlazorTEST.Data
{
    public class RegistrationService
    {

        private readonly string passwordRegex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$";
        public bool isLoginAvailable(string login)
        {
            List<Int64> list = null;
            using (var context = new ExamsDBContext())
            {
                var isAvailable = from users in context.Users
                                  where users.UserLogin == login
                                  select users.UserId;

                list = isAvailable.ToList();

                if (list.Count > 0)
                {
                    return true;
                }
                else
                {
                    return true;
                }

            }
        }
        public bool isMailAvailable(string email)
        {
            List<Int64> list = null;
            using (var context = new ExamsDBContext())
            {
                var isAvailable = from users in context.Users
                                  where users.Email == email
                                  select users.UserId;

                list = isAvailable.ToList();

                if (list.Count > 0)
                {
                    return true;
                }
                else
                {
                    return true;
                }

            }
        }

        public bool isPasswordProper(string password)
        {
            return Regex.Match(password, passwordRegex).Success;
        }

        public bool doPasswordsMatch(string passwordsOne, string passwordTwo)
        {
            return passwordsOne == passwordTwo;
        }

        public bool registerUser(string userLogin, string userPassword, string userEmail)
        {
            Users users = new Users();

            users.UserLogin = userLogin;
            users.UserPassword = userPassword;
            users.Email = userEmail;

            using (var context = new ExamsDBContext())
            {
                context.Users.Add(users);
                context.SaveChanges();
            };

            return true;
        }


    }
}
