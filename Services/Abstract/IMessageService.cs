using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstract
{
    public interface IMessageService
    {
        public bool Send(string number);
    }
}
