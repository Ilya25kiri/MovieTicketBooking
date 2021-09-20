using System;

namespace MovieTicketBoking.Exceptions
{
    public class NoSeatsException : Exception
    {
        public NoSeatsException(string message) : base(message)
        {
        }
    }
}
