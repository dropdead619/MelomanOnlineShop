using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstract
{
    public interface IMessageService
    {
        public void Send(string number);
    }
}
