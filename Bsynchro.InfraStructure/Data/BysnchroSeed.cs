using Bsynchro.Domain.Entities;
using Bsynchro.Services.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bsynchro.InfraStructure.Data
{
    public class BysnchroSeed
    {
        private readonly AccountsManager accountsManager;
        private readonly CustomersManager customersManager;
        private readonly TransactionsManager transactionsManager;
        public BysnchroSeed(AccountsManager accountsManager, CustomersManager customersManager, TransactionsManager transactionsManager)
        {
            this.accountsManager = accountsManager;
            this.customersManager = customersManager;
            this.transactionsManager = transactionsManager;
        }
        public async Task SeedAsync()
        {
            if (customersManager.IsEmpty())
            {
                Customer customer = new Customer()
                {
                    Name = "Mohammad",
                    Surname = "Dhainy",
                    Balance = 5000.0,
                };
                await customersManager.AddCustomer(customer);
                Account account = new Account
                {
                    Balance = 5000.0,
                    CustomerId = customer.CustomerId,
                };
                await accountsManager.AddAccount(account);
                Customer customer1 = new Customer()
                {
                    Name = "Jaafar",
                    Surname = "Dhainy",
                    Balance = 100.0
                };
                await customersManager.AddCustomer(customer1);
            }
        }
    }
}
