﻿using NetCore.Domain.Customers.Exceptions;

namespace NetCore.Domain.Customers
{
    public sealed record FullName
    {
        public string Value { get; init; }
        public FullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName) || fullName.Length < 3 || fullName.Length > 150)
            {
                throw new InvalidFullNameDomainException(fullName);
            }
            Value = fullName;
        }
        public static implicit operator string(FullName fullName) => fullName.Value;
        public static implicit operator FullName(string value) => new FullName(value);
    }
}
