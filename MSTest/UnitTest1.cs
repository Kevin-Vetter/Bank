using Bank.Models;
using Bank.Repository;

namespace MSTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //arrange
            BankRepo bank = new();
            bank.CreateAccount("name");
            //act
            string account = bank.ReadAllAccounts();
            //assert
            Assert.AreEqual("0. name\n", account);
        }
    }
}