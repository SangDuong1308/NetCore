namespace NetCore.Application.Exceptions
{
    public class OrderNotFoundApplicationException : ApplicationException
    {
        public OrderNotFoundApplicationException(Guid id) 
            : base($"Order with id '{id}' has not been found")
        {
        }
    }
}
