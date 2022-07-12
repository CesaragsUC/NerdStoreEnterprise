using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using NSE.Core.Messages;

namespace NSE.Core.Messages
{
    public class Event  :Message,INotification
    {

        public DateTime Timestamp { get; set; }

        public Event()
        {
            Timestamp = DateTime.Now;    
        }
    }
}
