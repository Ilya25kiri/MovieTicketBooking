using System;
namespace MovieTicketBoking.Exceptions
{
    public class NoEnoughtSeatsException : Exception
    {
        public NoEnoughtSeatsException(string message) : base(message)
        {
        }
    }
}
