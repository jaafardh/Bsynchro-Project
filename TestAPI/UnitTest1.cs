using Bsynchro.Domain.Repositories;
using Bsynchro.Services.Managers;
using Bsynchro_Api.Controllers;
using FakeItEasy;

namespace TestAPI
{
    public class UnitTest1
    {
        [Fact]
        public void TestGetUser()
        {
            var accountRepository = A.Fake<IAccountRepository>();
            var customerRepository = A.Fake<ICustomerRepository>();
            var tranactionRepository = A.Fake<ITransactionRepository>();
            var customerManager = new CustomersManager(null, customerRepository, tranactionRepository, accountRepository);

        }
    }
}