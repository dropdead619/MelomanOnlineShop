using Services.Abstract;
using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Services
{
    public class TwillioMessageService : IMessageService
    {
        public string Number { get; set; }
        public void Send(string number)
        {
            Number = number;
            //Убрал токен авторизации, т.к. он все равно скинется когда закину на гит
            string accountSid = "AC9cf7e06038b71632ddfa89adfc6f7979";
            string authToken = "";

            var randomCode = RandomCodeGenerationService.Generate(4);

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: $"Your 4-digits code is: {randomCode} ",
                from: new Twilio.Types.PhoneNumber("+16516153710"),
                to: new Twilio.Types.PhoneNumber(Number)
            );

            //Скорее всего эту часть можно вынести в UI
            bool checkUserCode = true;
            var SendedUserCode = " ";
            while (checkUserCode)
            {
                Console.WriteLine("Enter your 4-digits code: ");

                SendedUserCode = Console.ReadLine();
                if (SendedUserCode == randomCode)
                {
                    Console.WriteLine("Success!");
                    checkUserCode = false;
                }
                else
                {
                    Console.WriteLine("Incorrect 4-digits code, try again!");
                }
            }
        }
    }
}