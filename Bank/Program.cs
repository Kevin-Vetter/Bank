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
                            Continue();
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
                            Account selectedAccount = theBank.Accounts[accountNumber - 1];

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
                                        Continue();
                                        break;
                                    case ConsoleKey.W:
                                        subMenu = false;
                                        Console.Clear();
                                        Console.Write(theBank.Withdraw(selectedAccount, GetAmount()));
                                        Continue();
                                        break;
                                    case ConsoleKey.B:
                                        subMenu = false;
                                        Console.Clear();
                                        Console.WriteLine(theBank.Balance(selectedAccount));
                                        Continue();
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
                        case ConsoleKey.I:
                            menuLoop = true;
                            theBank.ApplyInterests();
                            Continue();
                            Console.Clear();
                            Menu(theBank.BankName);
                            break;
                        case ConsoleKey.X:
                            menuLoop = false;
                            Environment.Exit(0);
                            break;
                        case ConsoleKey.B:
                            menuLoop = true;
                            Console.Clear();
                            Console.WriteLine($"\nTotal balance of the bank is ${theBank.GetTotalBankBalance()}");
                            Continue();
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
            Console.WriteLine("************ MENU ************\nN. New Account\nE. Existing Account\nB. Total bank balance \nI. Apply Interests\nX. Exit");
        }


        static int GetAmount()
        {
            Console.WriteLine("\nInput number");
            int amount = TryParseInt();
            return amount;
        }

        static void Continue()
        {
            Console.WriteLine("\nPress any key to return to menu");
            Console.ReadKey();
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
        public double TotalBankBalance { get; }

        public Bank(string bankName)
        {
            BankName = bankName;
            Accounts = new List<Account>();
            TotalBankBalance = GetTotalBankBalance();
        }

        public double GetTotalBankBalance()
        {
            return Accounts.Sum(a => a.Balance);
        }

        public string CreateAccount(string accountName)
        {
            string ui = "";
            Console.WriteLine("Please choose desired account type\n1. Checking\n2. Savings\n3. Consumer\n");
            bool accLoop = true;
            while (accLoop)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        Account checkingAccount = new CheckingAccount(accountName, Accounts.Count + 1);
                        Accounts.Add(checkingAccount);
                        ui = $"A checking account with the name '{checkingAccount.AccountName}' and account number '{checkingAccount.Id}' has been created!";
                        accLoop = false;
                        break;
                    case ConsoleKey.D2:
                        Account savingsAccount = new SavingsAccount(accountName, Accounts.Count + 1);
                        Accounts.Add(savingsAccount);
                        ui = $"A savings account with the name '{savingsAccount.AccountName}' and account number '{savingsAccount.Id}' has been created!";
                        accLoop = false;
                        break;
                    case ConsoleKey.D3:
                        Account consumerAccount = new ConsumerAccount(accountName, Accounts.Count + 1);
                        Accounts.Add(consumerAccount);
                        ui = $"A consumer account with the name '{consumerAccount.AccountName}' and account number '{consumerAccount.Id}' has been created!";
                        accLoop = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Please choose desired account type\n1. Checking\n2. Savings\n3. Consumer\n");
                        accLoop = true;
                        break;
                }
            }
            Console.Clear();
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

        public void ApplyInterests()
        {
            foreach (Account acc in Accounts)
            {
                acc.ChargeInterests();
            }
            Console.WriteLine("\nInterests have been applied");
        }
        public Account GetAccountById(int id)
        {
            return Accounts.First(acc => acc.Id == id);
        }
    }
    public abstract class Account
    {
        public string AccountName { get; set; }
        public int Id { get; init; }
        public double Balance { get; set; }



        public abstract double ChargeInterests();

    }
    public class CheckingAccount : Account
    {
        public CheckingAccount(string accountName, int id)
        {
            AccountName = accountName;
            Id = id;
        }

        public override double ChargeInterests()
        {
            return Balance *= 1.005;
        }
    }
    public class SavingsAccount : Account
    {
        public SavingsAccount(string accountName, int id)
        {
            AccountName = accountName;
            Id = id;
        }

        public override double ChargeInterests()
        {
            if (Balance < 50000)
            {
                return Balance *= 1.01;
            }
            else if (Balance < 100000)
            {
                return Balance *= 1.02;
            }
            else
            {
                return Balance *= 1.03;
            }
        }
    }
    public class ConsumerAccount : Account
    {
        public ConsumerAccount(string accountName, int id)
        {
            AccountName = accountName;
            Id = id;
        }

        public override double ChargeInterests()
        {
            if (Balance <= 0)
            {
                return Balance *= 1.001;
            }
            else
            {
                return Balance *= 0.80;
            }
        }
    }
}
