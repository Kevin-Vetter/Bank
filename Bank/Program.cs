using System;

namespace Banken
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bank theBank = new Bank("$Banken$");
            while (true)
            {
                SplashScreen(theBank.BankName);
                Menu();

                bool menuLoop = true;
                do
                {
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.N:
                            Console.Clear();
                            Console.WriteLine("Input name of account: ");
                            string accountName = Console.ReadLine();
                            Console.WriteLine(theBank.CreateAccount(accountName));
                            Thread.Sleep(1500);
                            Console.Clear();
                            Menu();
                            break;

                        case ConsoleKey.E:
                            menuLoop = false;
                            Console.Clear();

                            Console.WriteLine("Please choose an account");
                            int i = 0;
                            foreach (string str in theBank.GetAccounts())
                            {
                                Console.WriteLine(i + ". " + str);
                                i++;
                            }
                            int accountNimber = Convert.ToInt32(Console.ReadLine());
                            Console.Clear();
                            Account selectedAccount = theBank.Accounts[accountNimber];

                            bool subMenu = true;
                            do
                            {
                                Console.WriteLine("Choose an option\nD. Deposit\nW. Withdraw\nB. Balance \nBackspace. Go back");
                                switch (Console.ReadKey().Key)
                                {
                                    case ConsoleKey.D:
                                        subMenu = false;
                                        Console.Clear();
                                        Console.Write(theBank.Deposit(selectedAccount, GetAmount()));
                                        break;
                                    case ConsoleKey.W:
                                        subMenu = false;
                                        Console.Clear();
                                        Console.Write(theBank.Withdraw(selectedAccount, GetAmount()));
                                        break;
                                    case ConsoleKey.B:
                                        subMenu = false;
                                        Console.Clear();
                                        Console.WriteLine(theBank.Balance(selectedAccount));
                                        break;
                                    case ConsoleKey.Backspace:
                                        subMenu = false;
                                        Console.Clear();
                                        Menu();
                                        break;
                                    default:
                                        Console.Clear();
                                        break;
                                }
                            } while (subMenu);
                            break;

                        case ConsoleKey.X:
                            Environment.Exit(0);
                            break;

                        default:
                            Console.Clear();
                            break;
                    }
                } while (menuLoop);
                Thread.Sleep(500);
                Console.Clear();
            }
        }

        static void Menu()
        {
            Console.WriteLine("************ MENU ************\nN. New Account\nE. Existing Account\nX. Exit");
        }

        static void SplashScreen(string bankName)
        {
            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            Console.WriteLine($"Welcome to {bankName}, Enjoy your stay!");
            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
        }

        static int GetAmount()
        {
            Console.WriteLine("\nInput ammount");
            int amount = TryParseInt();
            return amount;
        }

        static public int TryParseInt()
        {
            int _value;
            while (!int.TryParse(Console.ReadLine(), out _value))
            {
                Console.Clear();
                Console.WriteLine("Not a number!");
                Thread.Sleep(500);
                Console.Clear();
                Console.WriteLine("Input ammount");
            }
            return _value;
        }
    }

    public class Bank
    {
        public string BankName { get; }
        public List<Account> Accounts { get; set; }

        public Bank(string bankName)
        {
            BankName = bankName;
            Accounts = new List<Account>();
        }

        public string CreateAccount(string accountName)
        {
            Account myAccount = new Account(accountName);
            Accounts.Add(myAccount);
            string ui = $"An account with the name '{myAccount.AccountName}' has been created!";
            return ui;
        }
        public string Deposit(Account selectedAccount, int depositAmount)
        {
            selectedAccount.Balance += depositAmount;

            string ui = $"${depositAmount} has been deposited to your account,\nyour balance is now: ${selectedAccount.Balance}";
            return ui;
        }
        public string Withdraw(Account selectedAccount, int withdrawAmount)
        {
            selectedAccount.Balance -= withdrawAmount;

            string ui = $"${withdrawAmount} has been withdrawn from your account,\nyour balance is now: ${selectedAccount.Balance}";
            return ui;
        }
        public string Balance(Account selectedAccount)
        {
            string ui = $"Your balance is ${selectedAccount.Balance}";
            return ui;
        }

        public List<string> GetAccounts()
        {
            List<string> names = new List<string>();
            foreach (Account acc in this.Accounts)
            {
                names.Add(acc.AccountName);
            }
            return names;
        }
    }
    public class Account
    {
        public string AccountName { get; set; }
        public int Balance { get; set; }

        public Account(string accountName)
        {
            this.AccountName = accountName;
        }
    }
}
