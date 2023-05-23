using IbanValidator.Extentions;
using SinKien.IBAN4Net;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace IbanValidator;

static class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            string iban = AskForIBAN();
            bool validIBAN = IBANUtil.ValidateIBAN(iban, iban.GetPZ());
            decimal pzIBAN = IBANUtil.GetValidator(iban);

            ShowIBANResult(validIBAN, pzIBAN);
        }
    }

    static string AskForIBAN()
    {
        string iban = string.Empty;
        bool isValidIBAN = false;
        do
        {
            iban = AnsiConsole.Ask<string>("Enter your [aqua]IBAN[/]:");
            isValidIBAN = Regex.Match(iban, @"\w\w\d+$").Success;
            if (!isValidIBAN)
            {
                AnsiConsole.MarkupLine("\n[red]Invalid IBAN format![/]");
                PressKeyToContinue();
            }
        }
        while (!isValidIBAN);

        return iban;
    }

    static void ShowIBANResult(bool isValidIBAN, decimal pz)
    {
        AnsiConsole.Clear();

        if (isValidIBAN)
            AnsiConsole.MarkupLine("Your IBAN is [aqua]valid[/]!");
        else
            AnsiConsole.MarkupLine("Your IBAN is [red]valid[/]!");

        AnsiConsole.MarkupLine($"Validaton number is: [aqua]{pz}[/]");

        AnsiConsole.WriteLine();

        PressKeyToContinue();
    }

    static void PressKeyToContinue()
    {
        AnsiConsole.MarkupLine("[grey]Press any key to continue...[/]");
        Console.ReadKey();
        AnsiConsole.Clear();
    }
}