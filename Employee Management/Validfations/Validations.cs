using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Employee_Management.Validfations
{
    public static class Validations
    {
        public static bool Matches(this string value, string pattern) 
            => Regex.IsMatch(value, pattern);

        public static string PhoneNumberPattern => @"^\d{3}-\d{3}-\d{4}$";

        public static bool IsValidPhoneNumber(this string phoneNumber)
        => phoneNumber.Matches(PhoneNumberPattern);
    }
}
