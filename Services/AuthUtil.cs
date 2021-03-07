using Data;
using Models;

namespace Services
{
    public class AuthUtil
    {
        public static bool Authorization(string number)
        {
            var userDataAccess = new UserDataAccess();
            var userList = userDataAccess.Select();
            foreach (var element in userList)
            {
                if (element.PhoneNumber == number)
                {
                   var sendingSms = new TwillioMessageService();
                   //var sendingSms = new SmsKzMessageService();
                    if (sendingSms.Send(number) == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public static bool Registration(string number)
        {
            var userDataAccess = new UserDataAccess();
            var userList = userDataAccess.Select();
            foreach (var element in userList)
            {
                if (element.PhoneNumber == number)
                {
                    return false;
                }
            }
            var sendingSms = new TwillioMessageService();
            //var sendingSms = new SmsKzMessageService();
            if (sendingSms.Send(number) == true)
            {
                var user = new User()
                {
                    PhoneNumber = number
                };
                userDataAccess.Insert(user);
                return true;
            }
            else
            {
                return false;
            }
            
            
        }
    }
}
