using Bank.Models;
using Bank.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Bank
{
    public class Bank
    {
        public string BankName { get; }
        public List<Account> Accounts { get; set; }
        public double TotalBankBalance { get; }
        /// <summary>
        /// Constructor for Bank
        /// </summary>
        /// <param name="bankName"></param>
        public Bank(string bankName)
        {
            BankName = bankName;
            Accounts = new List<Account>();
            TotalBankBalance = GetTotalBankBalance();
        }
        /// <summary>
        /// Gets the total value of account balances added together
        /// </summary>
        /// <returns>double</returns>
        public double GetTotalBankBalance()
        {
            return Accounts.Sum(a => a.Balance);
        }
        /// <summary>
        /// Promtps user for creations of account
        /// </summary>
        /// <param name="accountName"></param>
        /// <returns>A string used to notify user of what has happend</returns>
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
        /// <summary>
        /// Prompts user for depositing into an account
        /// </summary>
        /// <param name="selectedAccount"></param>
        /// <param name="depositAmount"></param>
        /// <returns>A string used to notify user of what has happend</returns>
        public string Deposit(Account selectedAccount, int depositAmount)
        {
            selectedAccount.Balance += depositAmount;

            string ui = $"${depositAmount} has been deposited to your account,\nyour balance is now: ${selectedAccount.Balance}";
            return ui;
        }
        /// <summary>
        /// Prompts user for withdrawl from an account
        /// </summary>
        /// <param name="selectedAccount"></param>
        /// <param name="withdrawAmount"></param>
        /// <returns>A string used to notify user of what has happend</returns>
        public string Withdraw(Account selectedAccount, int withdrawAmount)
        {
            selectedAccount.Balance -= withdrawAmount;

            string ui = $"${withdrawAmount} has been withdrawn from your account,\nyour balance is now: ${selectedAccount.Balance}";
            return ui;
        }
        /// <summary>
        /// Used for viewing account balance
        /// </summary>
        /// <param name="selectedAccount"></param>
        /// <returns>A string used to notify of their current balance</returns>
        public string Balance(Account selectedAccount)
        {
            string ui = $"Your balance is ${selectedAccount.Balance}";
            return ui;
        }
        /// <summary>
        /// Gets all accounts in the bank
        /// </summary>
        /// <returns>A list of all account numbers</returns>
        public List<int> GetAccounts()
        {
            List<int> numbers = new List<int>();
            foreach (Account acc in Accounts)
            {
                numbers.Add(acc.Id);
            }
            return numbers;
        }
        /// <summary>
        /// Used to apply interests to all accounts
        /// </summary>
        public void ApplyInterests()
        {
            foreach (Account acc in Accounts)
            {
                acc.ChargeInterests();
            }
            Console.WriteLine("\nInterests have been applied");
        }
        /// <summary>
        /// Finds an account based on the 'id' argument
        /// </summary>
        /// <param name="id"></param>
        /// <returns>First Account object where id matches</returns>
        public Account GetAccountById(int id)
        {
            return Accounts.First(acc => acc.Id == id);
        }
    }
}
