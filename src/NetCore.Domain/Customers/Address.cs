namespace NetCore.Domain.Customers
{
    public sealed record Address (string Street, string HouseNumber, string FlatNumber, string Country, string PostalCode);
}
