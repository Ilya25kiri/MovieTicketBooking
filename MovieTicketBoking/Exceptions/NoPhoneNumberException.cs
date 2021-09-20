using System;
namespace MovieTicketBoking.Exceptions
{
    public class NoPhoneNumberException:Exception
    {
        public NoPhoneNumberException(string message):base(message)
        {
        }
    }
}
