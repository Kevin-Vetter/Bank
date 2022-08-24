using Microsoft.Extensions.DependencyInjection;
using Repository.Bank;
using TheBank;
using TheBank.Models;
using TheBank.Repository;

ServiceProvider serviceProvider = new ServiceCollection()
            .AddSingleton<IBank, BankRepo>()
            .BuildServiceProvider();
Menu(serviceProvider);

static void Menu(ServiceProvider sp)
{
    Bank bank = new(sp.GetService<IBank>());

    do
    {
        Console.Clear();
        Console.WriteLine($"Velkommen til {bank.BankName}");
        MenuList();
        switch (Console.ReadKey().Key)
        {
            #region Code for creating account
            case ConsoleKey.O:
                Console.CursorVisible = false;
                SubMenuList();
                ConsoleKey type = Console.ReadKey().Key;
                Console.Clear();
                Console.CursorVisible = true;
                Console.WriteLine("Navn: ");
                string name = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(name))
                {
                    Console.Clear();
                    Console.WriteLine("Navn kan ikke være tomt!");
                    Console.WriteLine("Navn: ");
                    name = Console.ReadLine();
                }
                Account account = bank._bank.CreateAccount(name, type);
                Console.Clear();
                Console.CursorVisible = false;
                Console.WriteLine(account != null ? $"{account.AccountType} oprettet med navn {account.Name} og nummer {account.AccountNumber}" : "fejl");
                Continue();
                break;
            #endregion

            #region Code for depositing
            case ConsoleKey.U:
                Console.CursorVisible = true;
                Console.Clear();
                Console.WriteLine("Indtast kontonummer");
                int number = ValidateInt();
                Console.Clear();
                Console.WriteLine("Indtast beløb: ");
                double amount = ValidateDouble();
                Console.CursorVisible = false;
                bool checkNull = bank._bank.Deposit(number, amount).HasValue;
                Console.WriteLine(checkNull ? $"Saldo efter indsæt: {bank._bank.Balance(number):c}" : "Konto findes ikke");
                Continue();
                break;
            #endregion

            #region Code for withdrawl
            case ConsoleKey.I:
                try
                {
                    Console.CursorVisible = true;
                    Console.Clear();
                    Console.WriteLine("Indtast kontonummer");
                    number = ValidateInt();
                    Console.Clear();
                    Console.WriteLine("Indtast beløb: ");
                    amount = ValidateDouble();
                    Console.CursorVisible = false;
                    checkNull = bank._bank.Withdraw(number, amount) != null;
                    Console.WriteLine(checkNull ? $"Saldo efter hæv: {bank._bank.Balance(number):c}" : "Konto findes ikke");
                    Continue();
                }
                catch (OverdraftException exception)
                {
                    Console.WriteLine(exception.Message);
                    Continue();
                }
                break;
            #endregion

            #region Code for showing balance
            case ConsoleKey.S:
                Console.Clear();
                Console.WriteLine("Indtast kontonummer:");
                number = ValidateInt();
                checkNull = bank._bank.Balance(number).HasValue;
                Console.WriteLine(checkNull ? $"Saldo er: {bank._bank.Balance(number):c}" : "Konto findes ikke");
                Continue();
                break;
            #endregion

            #region Code for showing everything
            case ConsoleKey.B:
                Console.Clear();
                Console.WriteLine($"Bank: {bank.BankName}");
                Console.WriteLine($"Bank saldo: {bank._bank.BankBalance():c}");
                Continue();
                break;
            #endregion

            #region Code for charging interests
            case ConsoleKey.R:
                bank._bank.ChargeInterest();
                break;
            #endregion

            #region Code for account list
            case ConsoleKey.K:
                Console.Clear();
                foreach (AccountListItem accItem in bank._bank.GetAccountList())
                {
                    Console.WriteLine($"{accItem.Account.Name}\t{accItem.Account.AccountType}\t{accItem.Account.Balance}");
                }
                Continue();
                break;
            #endregion

            #region Code for exiting
            case ConsoleKey.Escape:
                Environment.Exit(0);
                break;
            #endregion

            #region Code for default
            default:
                Console.Clear();
                break;
            #endregion
        }
    } while (true);
}

static double ValidateDouble()
{
    double amount;
    while (!double.TryParse(Console.ReadLine(), out amount))
    {
        Console.Clear();
        Console.WriteLine("Ugyldigt input!");
        Console.WriteLine("Indtast beløb: ");
    }
    Console.Clear();
    return amount;
}

static int ValidateInt()
{
    int amount;
    while (!int.TryParse(Console.ReadLine(), out amount))
    {
        Console.Clear();
        Console.WriteLine("Ugyldigt indtasting!");
        Console.WriteLine("Indtast tal: ");
    }
    Console.Clear();
    return amount;
}

static void MenuList()
{
    Console.WriteLine(
    "O. Opret konto\n" +
    "U. Udbetaling\n" +
    "I. Indbetaling\n" +
    "S. Vis Saldo\n" +
    "B. Vis Bank\n" +
    "R. Applicer rente\n" +
    "K. Vis alle konti\n" +
    "ESC. Afslut");
}

static void SubMenuList()
{
    Console.Clear();
    Console.WriteLine(
    "1. Check konto\n" +
    "2. Opsparings konto\n" +
    "3. Mastercard konto");
}

static void Continue()
{
    Console.WriteLine("\nTryk på en tast for at forsætte");
    Console.ReadKey();
}