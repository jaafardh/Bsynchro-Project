using Bsynchro.Domain.Entities;
using Bsynchro.Domain.Repositories;
using Bsynchro.Services.DTOs;
using Bsynchro.Services.Managers;
using Bsynchro_Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TestAPI
{

    [TestClass]
    public class AddAccountTests
    {
        [Fact]
        public async Task AddAccount_ReturnsBadRequest_WhenCustomerRepositoryFailsToAddAccount()
        {
            // Arrange
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(repo => repo.AddAccount(It.IsAny<int>()))
                .ReturnsAsync((List<Account>)null); ;

            var customerManager = new CustomersManager(
                customerRepositoryMock.Object, null, null); 

            var controller = new AccountsController(customerManager);
            // Act
            var result = await controller.AddAccount(new UserInfoDTO{
                CustomerId = 1,
                InitalCredit = 100
            });
            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [Fact]
        public async Task AddAccount_ReturnsBadRequest_WhenNotEnoughAccounts()
        {
            // Arrange
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(repo => repo.AddAccount(It.IsAny<int>()))
                .ReturnsAsync(new List<Account>());

            var customerManager = new CustomersManager(
                customerRepositoryMock.Object, null, null);

            var controller = new AccountsController(customerManager);

            // Act
            var result = await controller.AddAccount(new UserInfoDTO
            {
                CustomerId = 1,
                InitalCredit = 100
            });
            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [Fact]
        public async Task AddAccount_ReturnsBadRequest_WhenInitialCreditGreaterThanBalance()
        {
            // Arrange
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(repo => repo.AddAccount(It.IsAny<int>()))
                .ReturnsAsync(new List<Account> { new Account(), new Account() });

            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            transactionRepositoryMock.Setup(repo => repo.Transact(It.IsAny<Transaction>()))
                .ReturnsAsync(-1);

            var customerManager = new CustomersManager(
                customerRepositoryMock.Object, transactionRepositoryMock.Object, null);

            var controller = new AccountsController(customerManager);

            // Act
            var result = await controller.AddAccount(new UserInfoDTO
            {
                CustomerId = 1,
                InitalCredit = 100
            });
            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [Fact]
        public async Task AddAccount_ReturnsOk_WhenSuccessful()
        {
            // Arrange
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(repo => repo.AddAccount(It.IsAny<int>()))
                .Returns(Task.FromResult(new List<Account> { new Account(), new Account { CustomerId=1,AccountId = 2} }));

            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            transactionRepositoryMock.Setup(repo => repo.Transact(It.IsAny<Transaction>()))
                .ReturnsAsync(1); // Positive value for a successful transaction

            var customerManager = new CustomersManager(
                customerRepositoryMock.Object, transactionRepositoryMock.Object,null);

            var controller = new AccountsController(customerManager);

            // Act
            var result = await controller.AddAccount(new UserInfoDTO
            {
                CustomerId = 1,
                InitalCredit = 100
            }); // return the id of the last account added
            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
    }
}