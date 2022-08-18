global using global::System;
global using global::System.Collections.Generic;
global using global::System.IO;
global using global::System.Linq;
global using global::System.Net.Http;
global using global::System.Threading;
global using global::System.Threading.Tasks;
using Bank.Models;
using Repository.Bank;

namespace Banken
{
    public class Program
    {
        /// <summary>
        /// This is where the magic happens!
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Repository.Bank.Bank theBank = new Repository.Bank.Bank("$Banken$");
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
                            Account selectedAccount = theBank.Accounts[TryParseInt() - 1];

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
        /// <summary>
        /// Simply prints the menu on the screen
        /// </summary>
        /// <param name="bankName"></param>
        static void Menu(string bankName)
        {
            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            Console.WriteLine($"Welcome to {bankName}, Enjoy your stay!");
            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            Console.WriteLine("************ MENU ************\nN. New Account\nE. Existing Account\nB. Total bank balance \nI. Apply Interests\nX. Exit");
        }
        /// <summary>
        /// User inputs a number (really useless)
        /// </summary>
        /// <returns>Result of 'TryParseInt()'</returns>
        static int GetAmount()
        {
            Console.WriteLine("\nInput number");
            int amount = TryParseInt();
            return amount;
        }
        /// <summary>
        /// Simply Waits for a users input, then does nothing
        /// </summary>
        static void Continue()
        {
            Console.WriteLine("\nPress any key to return to menu");
            Console.ReadKey();
        }
        /// <summary>
        /// Determines if input is an integer
        /// </summary>
        /// <returns>An integer</returns>
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
}