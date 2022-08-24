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
    if (sp == null)
    {
        Environment.Exit(0);
    }
    Bank bank = new(sp.GetService<IBank>());

    do
    {
        Console.Clear();
        Console.WriteLine($"Velkommen til {bank.BankName}");
        Console.CursorVisible = false;
        MenuList();
        switch (Console.ReadKey(true).Key)
        {
            #region Create account
            case ConsoleKey.D1 or ConsoleKey.NumPad1:
                Console.CursorVisible = false;
                SubMenuList();
                ConsoleKey type = Console.ReadKey(true).Key;
                Console.Clear();
                Console.CursorVisible = true;
                Console.WriteLine("Name: ");
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
                Console.ReadKey(true);
                break;
            #endregion

            #region Deposit
            case ConsoleKey.D2 or ConsoleKey.NumPad2:
                Console.CursorVisible = true;
                Console.Clear();
                Console.WriteLine("Indtast kontonummer");
                int number = ValidateInt();
                Console.Clear();
                Console.WriteLine("Indtast beløb: ");
                decimal amount = ValidateDecimal();
                Console.CursorVisible = false;
                bool checkNull = bank._bank.Deposit(number, amount).HasValue;
                Console.WriteLine(checkNull ? $"Saldo efter indsæt: {bank._bank.Balance(number):c}" : "Konto findes ikke");
                Console.ReadKey(true);
                break;
            #endregion

            #region Withdraw
            case ConsoleKey.D3 or ConsoleKey.NumPad3:
                try
                {

                    Console.CursorVisible = true;
                    Console.Clear();
                    Console.WriteLine("Indtast kontonummer");
                    number = ValidateInt();
                    Console.Clear();
                    Console.WriteLine("Indtast beløb: ");
                    amount = ValidateDecimal();
                    Console.CursorVisible = false;
                    checkNull = bank._bank.Withdraw(number, amount) != null;
                    Console.WriteLine(checkNull ? $"Saldo efter hæv: {bank._bank.Balance(number):c}" : "Konto findes ikke");
                    Console.ReadKey(true);
                }
                catch (OverdraftException exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.ReadKey(true);
                }
                catch (Exception) { }
                break;
            #endregion

            #region Balance
            case ConsoleKey.D4 or ConsoleKey.NumPad4:
                Console.Clear();
                Console.WriteLine("Indtast kontonummer:");
                number = ValidateInt();
                checkNull = bank._bank.Balance(number).HasValue;
                Console.WriteLine(checkNull ? $"Saldo er: {bank._bank.Balance(number):c}" : "Konto findes ikke");
                Console.ReadKey(true);
                break;
            #endregion

            #region Bank Name
            case ConsoleKey.D5 or ConsoleKey.NumPad5:
                Console.Clear();
                Console.WriteLine($"Bank: {bank.BankName}");
                Console.WriteLine($"Bank saldo: {bank._bank.BankBalance():c}");
                Console.ReadKey(true);
                break;
            #endregion

            #region Charge interest
            case ConsoleKey.D6 or ConsoleKey.NumPad6:
                bank._bank.ChargeInterest();
                break;
            #endregion

            #region Show all accounts
            case ConsoleKey.D7:
                Console.Clear();
                foreach (AccountListItem accItem in bank._bank.GetAccountList())
                {
                    Console.WriteLine($"{accItem.Account.Name}\t{accItem.Account.AccountType}\t{accItem.Account.Balance}");
                }
                Console.ReadKey(true);
                break;
            #endregion

            #region Exit
            case ConsoleKey.Escape:
                Environment.Exit(0);
                break;
            #endregion

            #region Default
            default:
                Console.Clear();
                break;
                #endregion
        }
    } while (true);
}

static decimal ValidateDecimal()
{
    decimal amount;
    while (!decimal.TryParse(Console.ReadLine(), out amount))
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
        Console.WriteLine("Ugyldigt input!");
        Console.WriteLine("Indtast beløb: ");
    }
    Console.Clear();
    return amount;
}

static void MenuList()
{
    List<string> menu = new()
    {
        "1. Create Account",
        "2. Deposit",
        "3. Withdraw",
        "4. Show balance",
        "5. Show bank",
        "6. Get interests",
        "7. Show all accounts"
    };

    foreach (string item in menu)
    {
        Console.WriteLine(item);
    }
}

static void SubMenuList()
{
    Console.Clear();
    List<string> subMenu = new()
    {
        "1. Checking account",
        "2. Savings account",
        "3. Master Card account"
    };

    foreach (string item in subMenu)
    {
        Console.WriteLine(item);
    }
}