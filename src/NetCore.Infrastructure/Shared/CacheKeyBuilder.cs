﻿namespace NetCore.Infrastructure.Shared
{
    public static class CacheKeyBuilder
    {
        public static string GetCustomerKey(Guid customerId) => $"customer:{customerId}";
    }
}