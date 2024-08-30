namespace Accounting.Domain.Warehouses.ValueObjects
{
    public class Phone
    {
        public string CountryCallingCode { get; private set; }
        public string PhoneNumber { get; private set; }

        public Phone(
            string countryCallingCode,
            string phoneNumber)
        {
            CountryCallingCode = countryCallingCode;
            PhoneNumber = phoneNumber;
        }
    }
}
