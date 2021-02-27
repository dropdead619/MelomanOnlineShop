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
            string accountSid = "AC9cf7e06038b71632ddfa89adfc6f7979";
            string authToken = "6db4baf7b474ee57d0a946f5ae0ed09e";

            var randomCode = RandomCodeGenerationService.Generate(4);

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: $"Your 4-digits code is: {randomCode} ",
                from: new Twilio.Types.PhoneNumber("+16516153710"),
                to: new Twilio.Types.PhoneNumber(Number)
            );
            Console.WriteLine(message.Sid);
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