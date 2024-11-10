using NetCore.Domain.Customers.Exceptions;

namespace NetCore.Domain.Customers
{
    public sealed record Age
    {
        public int Value { get; }
        public DateTime BirthDate { get; }
        public Age(DateTime dateOfBirth)
        {
            BirthDate = dateOfBirth;
            Value = CalculateAge(dateOfBirth, DateTime.Now);
        }

        private static int CalculateAge(DateTime birthDate, DateTime currentDate)
        {
            var age = currentDate.Year - birthDate.Year;
            if (birthDate.Date > currentDate.AddYears(-age)) age--;
            if (age < 18)
            {
                throw new InvalidCustomerAgeDomainException();
            }
            return age;
        }
    }
}
