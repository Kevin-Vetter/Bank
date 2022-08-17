using System;

namespace Banken
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bank theBank = new Bank("$Banken$");
            SplashScreen(theBank.BankName);
            Console.WriteLine(theBank.CreateAcount("Alan"));
            Console.WriteLine("Please choose an account");
            int i = 0;
            foreach (string str in theBank.GetAccounts())
            {
                Console.WriteLine(i+". "+str);
                i++;
            }
            int selectedAccount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(theBank.Deposit(theBank.Accounts[selectedAccount], 450));
            Console.WriteLine(theBank.Withdraw(theBank.Accounts[selectedAccount], 50));
        }

        static void SplashScreen(string bankName)
        {
            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            Console.WriteLine($"Welcome to {bankName}, Enjoy your stay!");
            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
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

        public string CreateAcount(string accountName)
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
