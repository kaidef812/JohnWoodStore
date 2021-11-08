using JohnWoodStore.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JohnWoodStore.Model
{
    class Validator
    {
        public static bool IsValidEmail(string email)
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = "(375)+[0-9]{9}";
            Match isMatch = Regex.Match(phoneNumber, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }

        public static bool IsNotEnableLogin(string login)
        {
            using (StoreContext db = new StoreContext())
            {
                var users = db.Users.ToList();
                return users.Exists(x => x.Login == login);
            }
        }
    }
}
