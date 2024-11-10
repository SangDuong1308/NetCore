﻿namespace NetCore.Domain.Orders.Exceptions
{
    public class MaximumQuantityExceededDomainException : DomainException
    {
        public MaximumQuantityExceededDomainException()
            : base("Maximum allowed quantity has been exceeded")
        {
            
        }
    }
}