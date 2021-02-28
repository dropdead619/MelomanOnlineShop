using Data;
using Models;
using Services.Abstract;
using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Services
{
    public class TwillioMessageService : IMessageService
    {
        public string Number { get; set; }
        public bool Send(string number,Actions actions)
        {
            Number = number;
            //Убрал токен авторизации, т.к. он все равно скинется когда закину на гит
            string accountSid = "ACaddac989f2f1e947d2615d5b598719a2";
            string authToken = "cbc41ca9b42be97590c9f8647ca5d49f";

            var randomCode = RandomCodeGenerationService.Generate(4);

            TwilioClient.Init(accountSid, authToken);
            if (actions == Actions.Authentication)
            {
                var userDataAccess = new UserDataAccess();
                var userList = userDataAccess.Select();
                foreach (var element in userList)
                {
                    if (element.PhoneNumber == number)
                    {
                        var message = MessageResource.Create(
                    body: $"Your 4-digits code is: {randomCode} ",
                    from: new Twilio.Types.PhoneNumber("+19287702279"),
                    to: new Twilio.Types.PhoneNumber($"+{Number}")
                );

                        var SendedUserCode = "";
                        Console.WriteLine("Enter your 4-digits code: ");

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

                var message = MessageResource.Create(
                    body: $"Your 4-digits code is: {randomCode} ",
                    from: new Twilio.Types.PhoneNumber("+19287702279"),
                    to: new Twilio.Types.PhoneNumber($"+{Number}")
                );

                var SendedUserCode = "";
                Console.WriteLine("Enter your 4-digits code: ");

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