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
                Menu(theBank.BankName);

                bool menuLoop;
                do
                {
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.N:
                            menuLoop = true;
                            Console.Clear();
                            Console.WriteLine("Input name of account: ");
                            string accountName = Console.ReadLine();
                            Console.WriteLine(theBank.CreateAccount(accountName));
                            Thread.Sleep(1500);
                            Console.Clear();
                            Menu(theBank.BankName);
                            break;

                        case ConsoleKey.E:
                            menuLoop = false;
                            Console.Clear();

                            Console.WriteLine("Please choose an account");
                            foreach (int accNum in theBank.GetAccounts())
                            {
                                Console.WriteLine(accNum);
                            }
                            int accountNumber = Convert.ToInt32(Console.ReadLine());
                            Console.Clear();
                            Account selectedAccount = theBank.Accounts[accountNumber-1];

                            bool subMenu;
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
                                        Console.WriteLine("Press any key to return to menu");
                                        Console.ReadKey();
                                        break;
                                    case ConsoleKey.Backspace:
                                        subMenu = false;
                                        Console.Clear();
                                        Menu(theBank.BankName);
                                        break;
                                    default:
                                        subMenu = true;
                                        Console.Clear();
                                        Menu(theBank.BankName);
                                        break;
                                }
                            } while (subMenu);
                            break;

                        case ConsoleKey.X:
                            menuLoop = false;
                            Environment.Exit(0);
                            break;
                        case ConsoleKey.B:
                            menuLoop = true;
                            Console.Clear();
                            Console.WriteLine($"\nTotal balance of the bank is ${theBank.GetTotalBankBalance()}");
                            Console.WriteLine("Press any key to return to menu");
                            Console.ReadKey();
                            Console.Clear();
                            Menu(theBank.BankName);
                            break;
                        default:
                            menuLoop = true;
                            Console.Clear();
                            Menu(theBank.BankName);
                            break;
                    }
                } while (menuLoop);
                Thread.Sleep(500);
                Console.Clear();
            }
        }

        static void Menu(string bankName)
        {
            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            Console.WriteLine($"Welcome to {bankName}, Enjoy your stay!");
            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            Console.WriteLine("************ MENU ************\nN. New Account\nE. Existing Account\nB. Total bank balance \nX. Exit");
        }


        static int GetAmount()
        {
            Console.WriteLine("\nInput number");
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
                Console.WriteLine("Input number");
            }
            return _value;
        }
    }

    public class Bank
    {
        public string BankName { get; }
        public List<Account> Accounts { get; set; }
        public int TotalBankBalance { get; }

        public Bank(string bankName)
        {
            BankName = bankName;
            Accounts = new List<Account>();
            TotalBankBalance = GetTotalBankBalance();
        }

        public int GetTotalBankBalance()
        {
            return Accounts.Sum(a => a.Balance);
        }

        public string CreateAccount(string accountName)
        {

            Account myAccount = new Account(accountName, Accounts.Count+1);
            Accounts.Add(myAccount);
            string ui = $"An account with the name '{myAccount.AccountName}' and the account number '{myAccount.Id}' has been created!";
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

        public List<int> GetAccounts()
        {
            List<int> names = new List<int>();
            foreach (Account acc in Accounts)
            {
                names.Add(acc.Id);
            }
            return names;
        }

        public Account GetAccountById(int id)
        {
            return Accounts.First(acc => acc.Id == id);
        }
    }
    public class Account
    {
        public string AccountName { get; set; }
        public int Id { get;}
        public int Balance { get; set; }

        public Account(string accountName, int id)
        {
            AccountName = accountName;
            Id = id;
        }
    }
}
