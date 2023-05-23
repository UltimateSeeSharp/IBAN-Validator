namespace IbanValidator.Extentions;

internal static class IBANUtil
{
    internal static bool ValidateIBAN(string iban, decimal pz)
    {
        return pz == GetValidator(iban);
    }

    internal static decimal GetValidator(string iban)
    {
        decimal countryCode = iban.GetCountryCode();
        decimal accountNr = iban.GetAccountNr();

        string calcNrText = accountNr.ToString() + countryCode.ToString() + "00";
        decimal calcNr = decimal.Parse(calcNrText);

        decimal remainder = calcNr % 97;
        decimal validator = 98 - remainder;

        return validator;
    }

    internal static int GetCountryCode(this string iban)
    {
        string value = iban.Substring(0, 2);
        string countryCodeText = value[0].ToLetterCode().ToString() + value[1].ToLetterCode().ToString();
        int countryCode = Convert.ToInt32(countryCodeText);
        return countryCode;
    }

    internal static decimal ToLetterCode(this char letter) => (int)letter - 55;

    internal static decimal GetAccountNr(this string iban)
    {
        string value = iban.Substring(4, iban.Length - 4);
        decimal accountNr = decimal.Parse(value);
        return accountNr;
    }

    internal static decimal GetPZ(this string iban)
    {
        return Convert.ToDecimal(iban.Substring(2, 2));
    }
}
