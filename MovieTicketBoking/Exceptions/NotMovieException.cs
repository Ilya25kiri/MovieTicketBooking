using System;
namespace MovieTicketBoking.Exceptions
{
    public class NotMovieException : Exception
    {
        public NotMovieException(string message) : base(message)
        {
        }
    }
}
