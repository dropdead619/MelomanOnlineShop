using Services.Abstract;
using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Services
{
    public class TwillioMessageService : IMessageService
    {
        public string Number { get; set; }
        public bool Send(string number)
        {
            Number = number;
            var accountSid = "AC9cf7e06038b71632ddfa89adfc6f7979";
            var authToken = "ef8e9d76f384c05b47f5831a8e594aa4";

            var randomCode = RandomCodeGenerationService.Generate(4);

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                    body: $"Your 4-digits code is: {randomCode} ",
                    //from: new Twilio.Types.PhoneNumber("+19287702279"),
                    from: new Twilio.Types.PhoneNumber("+16516153710"),                  
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
}