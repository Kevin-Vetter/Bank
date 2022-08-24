using System.Xml.Linq;
using TheBank;
using TheBank.Models;
using TheBank.Repository;

namespace TheBankTest
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("Kevin", ConsoleKey.D1)]
        [InlineData("Kevin", ConsoleKey.D2)]
        [InlineData("Kevin", ConsoleKey.D3)]
        public void Test_if_account_created(string name, ConsoleKey key)
        {
            //Arrange
            BankRepo bankRepo = new();

            // Act
            AccountListItem acc = new(bankRepo.CreateAccount(name, key));

            // Assert
            Assert.All(bankRepo.GetAccountList(), x => Assert.Contains(acc.Account.Name, x.Account.Name));
        }

        [Fact]
        public void Test_for_deposit_from_account()
        {
            // Arrange
            BankRepo bankRepo = new();

            // Act
            bankRepo.CreateAccount("Kevin", ConsoleKey.D1);
            
            // Assert
            Assert.Equal(100, bankRepo.Deposit(0, 100));
        }

        [Fact]
        public void Test_for_withdraw_from_account()
        {
            // Arrange
            BankRepo bankRepo = new();

            // Act
            bankRepo.CreateAccount("Kevin", ConsoleKey.D1);

            // Assert
            Assert.Equal(-100, bankRepo.Withdraw(0, 100));
        }

        [Fact]
        public void Test_account_balance()
        {
            //Arrange
            BankRepo bankRepo = new();
            bankRepo.CreateAccount("Kevin", ConsoleKey.D1);
            
            // Act
            bankRepo.Deposit(0, 100);
            
            // Assert
            Assert.Equal(100, bankRepo.Balance(0));
        }

        [Fact]
        public void Test_bank_balance()
        {
            // Arrange
            BankRepo bankRepo = new();
            bankRepo.CreateAccount("Kevin", ConsoleKey.D1);
            bankRepo.CreateAccount("Kevin", ConsoleKey.D1);
            
            // Act
            bankRepo.Deposit(0, 100);
            bankRepo.Deposit(1, 100);
            
            // Assert
            Assert.Equal(200, bankRepo.BankBalance());
        }

        [Theory]
        [InlineData("Kevin", ConsoleKey.D1, 100)]
        [InlineData("Kevin", ConsoleKey.D2, 100)]
        [InlineData("Kevin", ConsoleKey.D3, 100)]
        [InlineData("Kevin", ConsoleKey.D3, 51000)]
        [InlineData("Kevin", ConsoleKey.D3, 110000)]
        public void Test_balance_after_charging_interest(string name, ConsoleKey key, int amount)
        {
            // Arrange
            BankRepo bankRepo = new();
            Account account = bankRepo.CreateAccount(name, key);

            // Act
            bankRepo.Deposit(account.AccountNumber, amount);
            bankRepo.ChargeInterest();
            
            // Assert
            Assert.Equal(account?.ChargeInterest(), bankRepo?.Balance(0));
        }

        [Fact]
        public void Test_if_same_account()
        {
            // Arrange
            BankRepo bankRepo = new();
            AccountListItem createdAcc = new(bankRepo.CreateAccount("", ConsoleKey.D1));
            
            // Act
            AccountListItem acc = bankRepo.GetAccountList().Find(x => x.Account.AccountNumber == 0);
            
            // Assert
            Assert.NotSame(createdAcc, acc);
        }

    }
}