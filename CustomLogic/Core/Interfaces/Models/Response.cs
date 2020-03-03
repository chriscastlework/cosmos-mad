
using System.Collections.Generic;

namespace  CustomLogic.Core.Interfaces.Models
{

    public class Response<T>
    {
        public Response()
        {
            Messages = new List<Message>();
            Success = false;
        }

        public void LogError(string error)
        {
            Messages.Add(new Message
            {
                MessageText = error,
                SeverityLevel = Error
            });
        }

        private const string Warn = "warn";
        private const string Info = "info";
        private const string Error = "error";

        public List<Message> Messages { get; }

        public bool Success { get; set; }
        public T Body { get; set; } 
    }

    public class Message
    {
        public string MessageText { get; set; }
        public string SeverityLevel { get; set; }
    }
}
