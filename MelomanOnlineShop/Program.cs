using Models.Abstract;
using Services;
using System;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            //UPD: Он прнинимает только верифицированные номера, я проверил на своем, работает. 
            //Если захочешь на своем пк проверить, можешь токены поменять на свой твилио внутри сервиса
            Console.WriteLine("Please, enter your phone number: (for example: +7XXXXXXXXXXX)");
            string userPhoneNumber = Console.ReadLine();
            var twilio = new TwillioMessageService();
            twilio.Send(userPhoneNumber);
        }
    }
}

