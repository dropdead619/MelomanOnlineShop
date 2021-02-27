using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class SmsKzMessageService : IMessageService
    {
        public bool Send(string number)
        {
            SMSC smsc = new SMSC();
            var randomCode = RandomCodeGenerationService.Generate(4);
            var sms = smsc.send_sms(number, randomCode);

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
}
