using Bank.Models;
using Bank.Repository;
using Banken;
using Microsoft.Extensions.DependencyInjection;
using Repository.Bank;

namespace unitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //arrange
            BankRepo bank = new();
            bank.CreateAccount("name");
            //act
            string account = bank.ReadAllAccounts();
            //assert
            Assert.Equal("0. name\n", account);
        }

        [Theory]
        [InlineData("Max")]
        [InlineData("Elias")]
        [InlineData("Kevin")]
        [InlineData("Brian")]
        [InlineData("Thomas")]
        public void Test2(string name)
        {
            //arrange
            BankRepo bank = new();
            bank.CreateAccount(name);
            //act
            string account = bank.ReadAllAccounts();
            //assert
            Assert.Equal($"0. {name}\n", account);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(10)]
        public void Test3(int a)
        {
            Assert.InRange(a, 5, 10);
        }

        [Fact]
        public void Test_for_interest()
        {
            // arrange
            ServiceProvider serviceProvider = new ServiceCollection()
        .AddSingleton<IBankRepo, BankRepo>()
        .BuildServiceProvider();

            Repository.Bank.daBank bank = new Repository.Bank.daBank("$Banken$", serviceProvider.GetService<IBankRepo>());


            bank.CreateAccount("b");
            bank.CreateAccount("c");
        
            // act
            bank.ApplyInterests();
            //assert
        }
    }
}