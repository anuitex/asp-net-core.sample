namespace WebApi.BusinessLogic.Exceptions
{
    public class NotFoundException: BaseException
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
