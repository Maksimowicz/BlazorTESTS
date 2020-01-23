using BlazorTEST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTEST.Data
{
    public class LoginHandlerService
    {
      
        public Int64 userId { get; private set; }

        public bool login(string login, string password)
        {
            List<Int64> list = null;
            using (var context = new ExamsDBContext())
            {
                var userQuery = from users in context.Users
                                where users.UserLogin == login &&
                                      users.UserPassword == password
                                select users.UserId;


                list = userQuery.ToList();

                if (list.Count() >0)
                {
                    userId = list[0];
                }

                return userId != 0 ? true : false;

            }
        }

        public bool logout()
        {
            bool success = false;

            if(userId != 0)
            {
                userId = 0;
                success = true;

            }
            return success;
        }

        public bool isLogged()
        {
            return userId != 0 ? true : false;
        }

       
    }
}
