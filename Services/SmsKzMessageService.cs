using Data;
using Models;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class SmsKzMessageService : IMessageService
    {
        public bool Send(string number, Actions actions)
        {
            SMSC smsc = new SMSC();
            var randomCode = RandomCodeGenerationService.Generate(4);
            if (actions == Actions.Authentication)
            {
                var userDataAccess = new UserDataAccess();
                var userList = userDataAccess.Select();
                foreach (var element in userList)
                {
                    if (element.PhoneNumber == number)
                    {
                        smsc.send_sms(number, randomCode);
                        var SendedUserCode = "";
                        Console.WriteLine("Введите 4-х значный код из смс: ");

                        SendedUserCode = Console.ReadLine();
                        if (SendedUserCode == randomCode)
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
            else
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
                smsc.send_sms(number, randomCode);

                var SendedUserCode = "";
                Console.WriteLine("Введите 4-х значный код из смс: ");

                SendedUserCode = Console.ReadLine();
                if (SendedUserCode == randomCode)
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
}
