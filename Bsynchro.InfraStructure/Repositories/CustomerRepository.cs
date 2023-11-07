using Bsynchro.Domain.Entities;
using Bsynchro.Domain.Repositories;
using Bsynchro.InfraStructure.Data;
using Bsynchro.InfraStructure.Repositories.Base;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Bsynchro.InfraStructure.Repositories
{
    public class CustomerRepository : Repository<Customer, BsynchroDatabaseContext>, ICustomerRepository
    {
        private readonly BsynchroDatabaseContext dbContext;
        public CustomerRepository(BsynchroDatabaseContext dbContext, ILogger logger) : base(dbContext, logger)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Account>> AddAccount(int customerId) 
        {
            var customer = await db.Customers.FindAsync(customerId);
            if (customer == null)
            {
                return null;
            }
            customer.Accounts = db.Accounts.Where(x => x.CustomerId == customerId).ToList();
            if(customer.Accounts.Count == 0)
            {
                return null; // the customer must has an initial account 
            }
            Account newAccount = new Account
            {
                CustomerId = customerId,
                Customer = customer,
            };
            await db.AddAsync(newAccount);
            customer.Accounts.Add(newAccount);
            try
            {
                await db.SaveChangesAsync();
            }
            catch
            {
                return null;
            }
            return db.Accounts.Where(x=> x.CustomerId == customerId).ToList();
        }

        public int Count()
        {
            return db.Customers.Count();
        }
    }
}
