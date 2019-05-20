using System;

namespace WebApi.BusinessLogic.Exceptions
{
    public class BaseException: ApplicationException
    {
        public BaseException(string message) : base(message)
        {
        }
    }
}
