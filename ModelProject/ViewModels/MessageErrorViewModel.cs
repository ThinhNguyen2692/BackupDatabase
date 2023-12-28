using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.ViewModels
{
    public enum MessageStatus
    {
        None,
        Success,
        Error,
        FTPFail
    }
    public class MessageBusViewModel
    {
        public MessageStatus MessageStatus { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
    }
}
